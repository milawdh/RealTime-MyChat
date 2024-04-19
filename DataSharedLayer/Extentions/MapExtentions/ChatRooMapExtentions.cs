using Domain.Enums;
using Domain.Entities;
using Domain.Profiles;
using Domain.DataLayer.UnitOfWorks;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.Chat.Message;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using Domain.DataLayer.Helpers;
using DomainShared.Extentions.Utility;

namespace DomainShared.Extentions.MapExtentions
{
    public static class ChatRooMapExtentions
    {
        /// <summary>
        /// Gets ChatRoomImage With a query from DataBase For notification Or ChatRoom Init Item
        /// </summary>
        /// <param name="chatroom">IQueryable that indicates Just One ChatRoom</param>
        /// <param name="currentUserId">Current User's Id</param>
        /// <returns></returns>
        public static string? GetChatRoomImage(this IQueryable<TblChatRoom> chatroom, Guid currentUserId)
        {
            return NavigationProfile.Resources + chatroom.GetChatRoomImageQuery(currentUserId).FirstOrDefault();
        }

        /// <summary>
        /// Gets ChatRoomImage With a query from DataBase For notification Or ChatRoom Init Item
        /// </summary>
        /// <param name="chatroom">ChatRoom That Includes It's Members and their Profile and it's own's Pic</param>
        /// <param name="currentUserId">Current User's Id</param>
        /// <returns></returns>
        public static string? GetChatRoomImage(this TblChatRoom chatroom, Guid currentUserId)
        {
            var res = NavigationProfile.Resources;
            switch (chatroom.Type)
            {
                case ChatRoomType.Private:
                case ChatRoomType.SecretChat:
                    res += chatroom.TblUserChatRoomRels
                    .FirstOrDefault(i => i.UserId != currentUserId)?.User?.ProfileImageUrlNavigation?.Url;
                    break;
                case ChatRoomType.Group:
                case ChatRoomType.Channel:
                    res += chatroom.ProfileImage?.Url;
                    break;
                default:
                    break;
            }
            return res;
        }

        public static string GetChatRoomTitle(this TblChatRoom chatRoom, Guid currentUserId)
        {
            var res = "";
            switch (chatRoom.Type)
            {
                case ChatRoomType.Private:
                case ChatRoomType.SecretChat:
                    res = chatRoom.TblUserChatRoomRels.FirstOrDefault(x => x.UserId != currentUserId).User.Name;
                    break;
                case ChatRoomType.Group:
                case ChatRoomType.Channel:
                    res = chatRoom.ChatRoomTitle;
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// Gets Many ChatRooms's Init Dtos For ChatRoomLists Item
        /// </summary>
        /// <param name="chatRooms">User Chat Rooms</param>
        /// <param name="currentUserId">CurrentUserId</param>
        /// <returns></returns>
        public static List<InitChatRoom> MapToInitChatRoom(this IQueryable<TblChatRoom> chatRooms, Guid currentUserId, Core core)
        {
            var resChatRooms = chatRooms.Include(x => x.TblMessages.OrderByDescending(c => c.CreatedDate).Take(1))
                .AsSplitQuery()
                .AsParallel()
                .ToList()
                .OrderByDescending(x => x.TblMessages?.FirstOrDefault()?.CreatedDate)
                .ToList();

            var result = resChatRooms.Select(i =>
              {
                  InitChatRoom res = i.Adapt<InitChatRoom>();
                  res.Name = i.GetChatRoomTitle(currentUserId);
                  var lastSeenMessageDate = i.TblUserChatRoomRels.FirstOrDefault(c => c.UserId == currentUserId)!.LastSeenMessage?.CreatedDate;
                  res.NotSeenMessagesCount = i.Id.GetNotSeenMessagesQuery(core, lastSeenMessageDate, currentUserId).Count();
                  res.Pic = i.GetChatRoomImage(currentUserId);
                  res.IsNew = lastSeenMessageDate == null;
                  return res;
              })
              .ToList();

            return result;
        }

        /// <summary>
        /// Gets a ChatRoom's Init Dto For ChatRoomLists Item
        /// </summary>
        /// <param name="currentUserId">Current UserId</param>
        /// <param name="chatRooms">Chat Room Entity</param>
        /// <returns></returns>
        public static InitChatRoom MapToInitChatRoom(this TblChatRoom chatRoom, Guid currentUserId, Core core)
        {
            return MapToInitChatRoom(new List<TblChatRoom>() { chatRoom }.AsQueryable(), currentUserId, core).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tblMessage"></param>
        /// <param name="core"></param>
        /// <returns></returns>
        public static RecieveMessageNotificationDto MapToRecieveMessageNotificationDto(this TblMessage tblMessage, Core core)
        {
            var notification = tblMessage.Adapt<RecieveMessageNotificationDto>();

            TblChatRoom chatRoom = core.TblChatRoom.Get(x => x.Id == tblMessage.RecieverChatRoomId,
                                     includes: v => v.Include(x => x.TblUserChatRoomRels).ThenInclude(x => x.User).ThenInclude(x => x.ProfileImageUrlNavigation))
                                    .FirstOrDefault();

            //Get Image
            notification.Image =
                chatRoom.GetChatRoomImage(tblMessage.CreatedById);


            if (chatRoom.Type == ChatRoomType.Private)
            {
                TblUser reciever = tblMessage.GetPrivateMessageRecieverQuery(core.TblUserChatRoomRel.Get());

                var recieverContacts = reciever.TblUserContactsContactUsers;

                if (recieverContacts.Any(v => v.ContactUserId == tblMessage.CreatedById))
                    notification.SenderUserName = recieverContacts.FirstOrDefault(x => x.ContactUserId == tblMessage.CreatedById)?.ContactName;
                else
                    notification.SenderUserName = tblMessage.CreatedBy.UserName;
            }
            else
                notification.SenderUserName = chatRoom.ChatRoomTitle;

            return notification;
        }



        /// <summary>
        /// Gets And Sorts A ChatRoom Messages And Maps To MessagesDto
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="chatRoomMapRepo"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public static List<MessagesDto> GetChatRoomMessageDtos(this Guid chatRoomId, Core core, Guid currentUserId, int? startRow = null)
        {
            IQueryable<TblUserChatRoomRel> map = core.TblUserChatRoomRel.Get(x => x.ChatRoomId == chatRoomId && x.UserId != currentUserId,
            includes: x => x.Include(v => v.LastSeenMessage));

            var chatRoomMessages = core.TblChatRoom.Get(x => x.Id == chatRoomId).GetChatRoomLazyMessages(startRow).AsSplitQuery().ToList();
            if (startRow != null)
            {
                int totalCount = core.TblMessage.Get(x => x.RecieverChatRoomId == chatRoomId).Count();
                return chatRoomMessages.Select(src =>
                   {
                       var res = src.Adapt<MessagesDto>();
                       res.Status = src.GetMessageStatusQuery(map);
                       res.IsEnd = startRow + 12 >= totalCount;
                       return res;
                   }).ToList();
            }
            else
                return chatRoomMessages.Select(src =>
                {
                    var res = src.Adapt<MessagesDto>();
                    res.Status = src.GetMessageStatusQuery(map);
                    return res;
                }).ToList();

        }

        /// <summary>
        /// Maps TblChatRoom To GroupChatRoom Dto
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="chatRoomMapRepo"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public static GroupChatRoomDto MapToGroupChatRoomDto(this TblChatRoom chatRoom, MainRepo<TblUserChatRoomRel> repo, Guid currentUserId)
        {
            var result = chatRoom.Adapt<GroupChatRoomDto>();
            result.NavbarText = chatRoom.GetNavbarText(repo, currentUserId);
            return result;
        }

        /// <summary>
        /// Maps TblChatRoom To PrivateChatRoom Dto
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="chatRoomMapRepo"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public static PrivateChatRoomDto MapToPrivateChatRoomDto(this TblChatRoom chatRoom, MainRepo<TblUserChatRoomRel> repo, Guid currentUserId)
        {
            var res = chatRoom.Adapt<PrivateChatRoomDto>();
            res.NavbarText = chatRoom.GetNavbarText(repo, currentUserId);
            return res;
        }

        public static ChannelChatRoomDto MapToChannelChatRoomDto(this TblChatRoom chatRoom, MainRepo<TblUserChatRoomRel> repo, Guid currentUserId)
        {
            var res = chatRoom.Adapt<ChannelChatRoomDto>();
            res.NavbarText = chatRoom.GetNavbarText(repo, currentUserId);
            return res;
        }

        /// <summary>
        /// Gets text That Will be shown on Navbar for User From ChatRoom
        /// </summary>
        /// <param name="chatRoom">Chat Room That User opened</param>
        /// <returns></returns>
        public static string GetNavbarText(this TblChatRoom chatRoom, MainRepo<TblUserChatRoomRel> repo, Guid currentUserId)
        {
            string result = "";
            switch (chatRoom.Type)
            {
                case Domain.Enums.ChatRoomType.Private:
                    var user = repo.Get(x => x.ChatRoomId == chatRoom.Id && x.UserId != currentUserId).FirstOrDefault()!.User;
                    if (user.IsOnline)
                        result = "Online";
                    else
                    {
                        if ((DateTime.Now - user.LastOnline).Days > 1)
                            result = user.LastOnline.ToString("yyyy/M/dd H:m");
                        else
                            result = user.LastOnline.ToString("H:m");
                    }
                    break;
                case Domain.Enums.ChatRoomType.Group:
                    var userNames = repo.Get(x => x.ChatRoomId == chatRoom.Id).Take(3).Select(i => i.User.UserName).ToList();
                    result = "You, " + string.Join(", ", userNames);
                    if (chatRoom.TblUserChatRoomRels.Count > 2)
                        result += "...";
                    break;
                case Domain.Enums.ChatRoomType.Channel:
                    result = repo.Get(x => x.ChatRoomId == chatRoom.Id).Count().FormatCount();
                    break;
                case Domain.Enums.ChatRoomType.SecretChat:
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

using Azure;
using Domain.Enums;
using Domain.Entities;
using Domain.Profiles;
using Domain.DataLayer.UnitOfWorks;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.Chat.Message;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using Services.Repositories;
using Domain.DataLayer.Helpers;

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
            return NavigationProfile.Resources + (chatroom.Type == ChatRoomType.Private ?
                     chatroom.TblUserChatRoomRels.Where(i => i.UserId != currentUserId)
                    .Select(v => v.User.ProfileImageUrlNavigation.Url)
                    .FirstOrDefault()
                    :
                     chatroom.ProfileImageId != null ? chatroom.ProfileImage.Url : null);
        }

        /// <summary>
        /// Gets Many ChatRooms's Init Dtos For ChatRoomLists Item
        /// </summary>
        /// <param name="chatRooms">User Chat Rooms</param>
        /// <param name="currentUserId">CurrentUserId</param>
        /// <returns></returns>
        public static List<InitChatRoom> MapToInitChatRoom(this IEnumerable<TblChatRoom> chatRooms, Guid currentUserId)
        {
            var result =
             chatRooms
             .OrderByDescending(x => x.TblMessages.OrderBy(c => c.CreatedDate).Last().CreatedDate)
             .Select(i =>
             {
                 InitChatRoom res = i.Adapt<InitChatRoom>();
                 var lastSeenMessageDate = i.TblUserChatRoomRels.FirstOrDefault(c => c.UserId == currentUserId)?.LastSeenMessage?.CreatedDate;
                 res.NotSeenMessagesCount = i.GetNotSeenMessagesQuery(lastSeenMessageDate, currentUserId).Count();
                 res.Pic = i.GetChatRoomImage(currentUserId);
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
        public static InitChatRoom MapToInitChatRoom(this TblChatRoom chatRoom, Guid currentUserId)
        {
            return MapToInitChatRoom(new List<TblChatRoom>() { chatRoom }, currentUserId).FirstOrDefault();
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
        public static List<MessagesDto> GetChatRoomMessageDtos(this TblChatRoom chatRoom, MainRepo<TblUserChatRoomRel> chatRoomMapRepo, Guid currentUserId, int? startRow = null)
        {
            IQueryable<TblUserChatRoomRel> map = chatRoomMapRepo.Get(x => x.ChatRoomId == chatRoom.Id && x.UserId != currentUserId,
            includes: x => x.Include(v => v.LastSeenMessage));

            var chatRoomMessages = chatRoom.TblMessages.OrderByDescending(x => x.CreatedDate);
            if (startRow != null)
            {
                int totalCount = chatRoomMessages.Count();
                return chatRoomMessages.Skip(startRow.Value).Take(12).Select(src =>
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
        public static GroupChatRoomDto MapToGroupChatRoomDto(this TblChatRoom chatRoom, Guid currentUserId)
        {
            var result = chatRoom.Adapt<GroupChatRoomDto>();

            return result;
        }

        /// <summary>
        /// Maps TblChatRoom To PrivateChatRoom Dto
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="chatRoomMapRepo"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public static PrivateChatRoomDto MapToPrivateChatRoomDto(this TblChatRoom chatRoom, Guid currentUserId)
        {
            chatRoom.TblUserChatRoomRels = chatRoom.TblUserChatRoomRels.OrderByDescending(x => x.UserId != currentUserId).ToList();
            var res = chatRoom.Adapt<PrivateChatRoomDto>();

            return res;
        }
    }
}

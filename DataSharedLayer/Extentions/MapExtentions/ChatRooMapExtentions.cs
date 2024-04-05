using Azure;
using Domain.Enums;
using Domain.Models;
using Domain.Profiles;
using Domain.UnitOfWorks;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Extentions.Query;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace DomainShared.Extentions.MapExtentions
{
    public static class ChatRooMapExtentions
    {
        private static Core _core = new();

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
                     chatroom.TblUserChatRoomRel.Where(i => i.UserId != currentUserId)
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
        public static List<InitChatRoom> GetInitChatRoom(Guid currentUserId, IEnumerable<TblChatRoom> chatRooms)
        {
            var result =
             chatRooms
             .OrderByDescending(x => x.TblMessage.OrderBy(c => c.SendAt).Last().SendAt)
             .Select(i =>
             {
                 InitChatRoom res = i.Adapt<InitChatRoom>();
                 var lastSeenMessageDate = i.TblUserChatRoomRel.FirstOrDefault(c => c.UserId == currentUserId)?.LastSeenMessage?.SendAt;
                 if (lastSeenMessageDate is not null)
                 {
                     res.NotSeenMessagesCount = i.TblMessage.Where(x => x.SendAt > lastSeenMessageDate && x.SenderUserId != currentUserId).Count();
                 }
                 else
                 {
                     res.NotSeenMessagesCount = i.TblMessage.Where(x => x.SenderUserId != currentUserId).Count();
                 }
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
        public static InitChatRoom GetInitChatRoom(Guid currentUserId, TblChatRoom chatRoom)
        {
            return GetInitChatRoom(currentUserId, new List<TblChatRoom>() { chatRoom }).FirstOrDefault();
        }

        public static RecieveMessageNotificationDto GetRecieveMessageNotificationDto(this TblMessage tblMessage)
        {
            var notification = tblMessage.Adapt<RecieveMessageNotificationDto>();

            TblChatRoom chatRoom = _core.TblChatRoom.Get(x => x.Id == tblMessage.RecieverChatRoomId,
                                     includes: v => v.Include(x => x.TblUserChatRoomRel).ThenInclude(x => x.User).ThenInclude(x => x.ProfileImageUrlNavigation))
                                    .FirstOrDefault();

            //Get Image
            notification.Image =
                chatRoom.GetChatRoomImage(tblMessage.SenderUserId);


            if (chatRoom.Type == ChatRoomType.Private)
            {
                TblUsers reciever = _core.GetPrivateMessageRecieverQuery(tblMessage).FirstOrDefault()!;

                var recieverContacts = reciever.TblUserContactsContactUser;

                if (recieverContacts.Any(v => v.ContactUserId == tblMessage.SenderUserId))
                    notification.SenderUserName = recieverContacts.FirstOrDefault(x => x.ContactUserId == tblMessage.SenderUserId)?.ContactName;
                else
                    notification.SenderUserName = tblMessage.SenderUser.UserName;
            }
            else
                notification.SenderUserName = chatRoom.ChatRoomTitle;

            return notification;
        }
    }
}

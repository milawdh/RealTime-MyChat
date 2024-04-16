using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataLayer.Helpers
{
    public static class QueryHelpers
    {
        /// <summary>
        /// Gets A Private Message Reciever (Other Side of message's sender user)
        /// </summary>
        /// <param name="tblMessage"></param>
        /// <param name="chatRoomMap"></param>
        /// <returns></returns>
        public static TblUser GetPrivateMessageRecieverQuery(this TblMessage tblMessage, IQueryable<TblUserChatRoomRel> chatRoomMap)
        {
            return chatRoomMap
                 .Where(v => v.ChatRoomId == tblMessage.RecieverChatRoomId && v.UserId != tblMessage.CreatedById)
                 .Include(v => v.User).ThenInclude(c => c.TblUserContactsContactUsers)
                 .Select(v => v.User).FirstOrDefault();
        }

        /// <summary>
        /// Gets A ChatRoom Image
        /// </summary>
        /// <param name="chatroom"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public static string GetChatRoomImageQuery(this IQueryable<TblChatRoom> chatroom, Guid currentUserId)
        {
            return chatroom.Select(x =>
                      x.Type == ChatRoomType.Private ?
                      x.TblUserChatRoomRels.Where(i => i.UserId != currentUserId)
                     .Select(v => v.User.ProfileImageUrlNavigation.Url).FirstOrDefault()
                     :
                      x.ProfileImageId != null ? x.ProfileImage.Url : null).FirstOrDefault();
        }

        /// <summary>
        /// Gets A TblChatRoom Not Seen Messages by given UserId
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <param name="lastSeenMessageDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IQueryable<TblMessage> GetNotSeenMessagesQuery(this TblChatRoom chatRoom, DateTime? lastSeenMessageDate, Guid userId)
        {
            if (lastSeenMessageDate is not null)
            {
                return chatRoom.TblMessages.Where(x => x.CreatedDate > lastSeenMessageDate && x.CreatedById != userId).AsQueryable();
            }
            else
            {
                return chatRoom.TblMessages.Where(x => x.CreatedById != userId).AsQueryable();
            }
        }

        /// <summary>
        /// Gets Messages Status That Is Read Or Not
        /// </summary>
        /// <param name="message"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static MessageStatus GetMessageStatusQuery(this TblMessage message, IQueryable<TblUserChatRoomRel> map)
        {
            if (message.RecieverChatRoom.Type == ChatRoomType.Private)
            {

                if (map.FirstOrDefault().LastSeenMessage == null)
                    return MessageStatus.Sent;

                return
                    map.FirstOrDefault().LastSeenMessage.CreatedDate >= message.CreatedDate ? MessageStatus.Read : MessageStatus.Sent;
            }
            else
            {
                if (map.Any(x => x.LastSeenMessage.CreatedDate >= message.CreatedDate))
                    return MessageStatus.Read;

                return MessageStatus.Sent;
            }
        }
    }
}

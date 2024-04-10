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
        public static TblUsers GetPrivateMessageRecieverQuery(this TblMessage tblMessage, IQueryable<TblUserChatRoomRel> chatRoomMap)
        {
            return chatRoomMap
                 .Where(v => v.ChatRoomId == tblMessage.RecieverChatRoomId && v.UserId != tblMessage.SenderUserId)
                 .Include(v => v.User).ThenInclude(c => c.TblUserContactsContactUser)
                 .Select(v => v.User).FirstOrDefault();
        }

        public static string GetChatRoomImageQuery(this IQueryable<TblChatRoom> chatroom, Guid currentUserId)
        {
            return chatroom.Select(x =>
                      x.Type == ChatRoomType.Private ?
                      x.TblUserChatRoomRel.Where(i => i.UserId != currentUserId)
                     .Select(v => v.User.ProfileImageUrlNavigation.Url).FirstOrDefault()
                     :
                      x.ProfileImageId != null ? x.ProfileImage.Url : null).FirstOrDefault();
        }

        public static IQueryable<TblMessage> GetNotSeenMessagesQuery(this TblChatRoom chatRoom, DateTime? lastSeenMessageDate, Guid currentUserId)
        {
            if (lastSeenMessageDate is not null)
            {
                return chatRoom.TblMessage.Where(x => x.SendAt > lastSeenMessageDate && x.SenderUserId != currentUserId).AsQueryable();
            }
            else
            {
                return chatRoom.TblMessage.Where(x => x.SenderUserId != currentUserId).AsQueryable();
            }
        }
    }
}

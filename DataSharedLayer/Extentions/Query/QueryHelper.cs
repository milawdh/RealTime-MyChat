using Domain.Enums;
using Domain.Models;
using Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DomainShared.Extentions.Query
{
    public static class QueryHelper
    {
        public static IQueryable<TblUserContacts> GetUserContactsQuery(this Core core, Guid userId)
        {
            return core.TblUserContacts.Get(x => x.ContactListOwnerId == userId);
        }

        public static IQueryable<TblUsers> GetPrivateMessageRecieverQuery(this Core core, TblMessage tblMessage)
        {
            return core.TblUserChatRoomRel
                 .Get(v => v.ChatRoomId == tblMessage.RecieverChatRoomId && v.UserId != tblMessage.SenderUserId,
                 includes: x => x.Include(v => v.User).ThenInclude(c => c.TblUserContactsContactUser))
                 .Select(v => v.User);
        }

        public static IQueryable<string> GetChatRoomImageQuery(this IQueryable<TblChatRoom> chatroom, Guid currentUserId)
        {
            return chatroom.Select(x =>
                      x.Type == ChatRoomType.Private ?
                      x.TblUserChatRoomRel.Where(i => i.UserId != currentUserId)
                     .Select(v => v.User.ProfileImageUrlNavigation.Url).FirstOrDefault()
                     :
                      x.ProfileImageId != null ? x.ProfileImage.Url : null);
        }

    }
}

using Domain.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Extentions.Query
{
    public static class QueryHelper
    {
        //public static IQueryable<TblUserContacts> GetUserContactsQuery(this Guid userId)
        //{
        //    return core.TblUserContacts.Get(x => x.ContactListOwnerId == userId);
        //}

        //public static IQueryable<TblUsers> GetPrivateMessageRecieverQuery(this TblMessage tblMessage)
        //{
        //    return core.TblUserChatRoomRel
        //         .Get(v => v.ChatRoomId == tblMessage.RecieverChatRoomId && v.UserId != tblMessage.SenderUserId,
        //         includes: x => x.Include(v => v.User).ThenInclude(c => c.TblUserContactsContactUser))
        //         .Select(v => v.User);
        //}

        //public static IQueryable<string> GetChatRoomImageQuery(this IQueryable<TblChatRoom> chatroom, Guid currentUserId)
        //{
        //    return chatroom.Select(x =>
        //              x.Type == ChatRoomType.Private ?
        //              x.TblUserChatRoomRel.Where(i => i.UserId != currentUserId)
        //             .Select(v => v.User.ProfileImageUrlNavigation.Url).FirstOrDefault()
        //             :
        //              x.ProfileImageId != null ? x.ProfileImage.Url : null);
        //}

        //public static IQueryable<TblMessage> GetNotSeenMessagesQuery(this TblChatRoom chatRoom, DateTime? lastSeenMessageDate, Guid currentUserId)
        //{
        //    if (lastSeenMessageDate is not null)
        //    {
        //       return chatRoom.TblMessage.Where(x => x.SendAt > lastSeenMessageDate && x.SenderUserId != currentUserId).AsQueryable();
        //    }
        //    else
        //    {
        //       return chatRoom.TblMessage.Where(x => x.SenderUserId != currentUserId).AsQueryable();
        //    }
        //}
    }
}

using Domain.Models;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.Chat.Message;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Extentions
{
    public static class ChatRoomExtentions
    {
        public static List<string> GetChatRoomUserNames(this TblChatRoom chatRoom, Guid currentUserId)
        {
            return chatRoom
                .TblUserChatRoomRel
                .Where(x => x.UserId != currentUserId)
                .Select(x => x.User.UserName)
                .ToList();
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
                     res.NotSeenMessagesCount = i.TblMessage.Where(x=>x.SenderUserId ==  currentUserId).Count();
                 }

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
    }
}

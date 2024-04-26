using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.User;
using Mapster;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Extentions.MapExtentions
{
    public static class UserMapExtentions
    {

        /// <summary>
        /// Maps User Entity to UserInitDto that indicates all User's Details for Initializing
        /// </summary>
        /// <param name="user">USer Entity</param>
        /// <param name="ChatRoomsWithOutMessages">User's AllChatRooms That Indicates Their Messages</param>
        /// <returns>All Current User's Details for Initializing</returns>

        public static UserInitDto MapToUserInitDto(this TblUser user, IQueryable<TblChatRoom> ChatRoomsWithOutMessages, Core core)
        {
            UserInitDto result = user.Adapt<UserInitDto>();
            result.ChatRooms = ChatRoomsWithOutMessages.MapToChatListItemDto(user.Id, core);

            return result;
        }
    }
}

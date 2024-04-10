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
        /// <param name="ChatRoomsWithMessages">User's AllChatRooms That Indicates Their Messages</param>
        /// <returns>All Current User's Details for Initializing</returns>

        public static UserInitDto MapToUserInitDto(this TblUsers user, IEnumerable<TblChatRoom> ChatRoomsWithMessages)
        {
            UserInitDto result = user.Adapt<UserInitDto>();
            result.ChatRooms = ChatRoomsWithMessages.MapToInitChatRoom(user.Id);

            return result;
        }
    }
}

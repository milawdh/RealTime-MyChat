using Domain.Enums;
using Domain.Entities;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.ChatRoom
{
    public class ChatListItemDto : IHasCustomMap
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        ///<summary>
        /// Map It With GetChatRoomImage Method
        /// </summary>
        public string Pic { get; set; }

        public int NotSeenMessagesCount { get; set; } = 0;
        public LastMessageDto LastMessage { get; set; }

        public bool IsNew { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblChatRoom, ChatListItemDto>.NewConfig()
                .IgnoreNonMapped(true)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.LastMessage, src => src.TblMessages.FirstOrDefault().Adapt<LastMessageDto>());
        }
    }
}

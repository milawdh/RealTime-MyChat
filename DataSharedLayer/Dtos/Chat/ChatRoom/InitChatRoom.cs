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
    public class InitChatRoom : IHasCustomMap
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        ///<summary>
        /// Map It With GetChatRoomImage Method
        /// </summary>
        public string Pic { get; set; }

        public int NotSeenMessagesCount { get; set; } = 0;
        public LastMessageDto LastMessage { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblChatRoom, InitChatRoom>.NewConfig()
                .IgnoreNonMapped(true)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Type == ChatRoomType.Group ? src.ChatRoomTitle :
                src.TblUserChatRoomRels.First().User.Name)
                .Map(dest => dest.LastMessage, src => src.TblMessages.OrderBy(x => x.CreatedDate).LastOrDefault().Adapt<LastMessageDto>());
        }
    }
}

using Domain.Enums;
using Domain.Models;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.Message
{
    public class RecieveMessageDto : IHasCustomMap
    {
        /// <summary>
        /// Message Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Message SenderId
        /// </summary>
        public Guid RecieverChatRoomId { get; set; }

        /// <summary>
        /// Message SenderName
        /// </summary>
        public string SenderUserName { get; set; }

        /// <summary>
        /// Message Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Message Sent date to long string
        /// </summary>
        public string Time { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblMessage, RecieveMessageDto>.NewConfig()
                .Map(dest => dest.Time, src => src.SendAt.ToLongTimeString() + " " + src.SendAt.ToShortDateString());
        }
    }
}

﻿using Domain.Enums;
using Domain.Models;
using Domain.UnitOfWorks;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.Message
{
    public class MessagesDto : IHasCustomMap
    {
        /// <summary>
        /// Message Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Message SenderId
        /// </summary>
        public Guid Sender { get; set; }

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

        /// <summary>
        /// Message Status
        /// </summary>
        public MessageStatus Status { get; set; }

        /// <summary>
        /// Message Reciever ChatRoom Id
        /// </summary>
        public Guid RecieverChatRoomId { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblMessage, MessagesDto>.NewConfig()
            .Map(dest => dest.Sender, src => src.SenderUserId)
            .Map(dest => dest.SenderUserName, src => src.SenderUser.Name)
            .Map(dest => dest.Time, src => src.SendAt.ToLongTimeString() + " " + src.SendAt.ToShortDateString())
            .Map(dest => dest.RecieverChatRoomId, src => src.RecieverChatRoomId);
        }
    }
}

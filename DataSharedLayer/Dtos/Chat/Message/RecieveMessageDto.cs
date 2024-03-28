using Domain.Enums;
using DomainShared.Dtos.Chat.ChatRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.Message
{
    public class RecieveMessageDto
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
        /// Message Sender ChatRoom
        /// </summary>
        public InitChatRoom SenderChatRoom { get; set; }
    }
}

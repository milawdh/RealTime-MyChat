using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.Message
{
    public class SendMessageDto
    {
        public string Body { get; set; }
        public Guid RecieverChatRoomId { get; set; }
    }
}

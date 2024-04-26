using Domain.Entities;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.ChatRoom
{
    public class ChatRoomSelectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Pic { get; set; }
    }
}

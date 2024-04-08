using Domain.Entities;
using DomainShared.Extentions.Utility;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.Message
{
    public class RecieveMessageNotificationDto : IHasCustomMap
    {
        public string Body { get; set; }
        public string? Image { get; set; }
        public string SenderUserName { get; set; }
        public Guid RecieverChatRoomId { get; set; }
        public string Time { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblMessage, RecieveMessageNotificationDto>
               .NewConfig()
                .Map(dest => dest.Time, src => src.SendAt.ToLongTimeString() + " " + src.SendAt.ToShortDateString())
               .Map(dest => dest.Body, src => src.Body.FormatLength(10));
        }
    }
}

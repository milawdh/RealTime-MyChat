﻿using Domain.Enums;
using Domain.Entities;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.Message
{
    public class LastMessageDto : IHasCustomMap
    {
        public string Body { get; set; }
        public string Time { get; set; }
        public string SenderUserName { get; set; }
        public void ConfigMap()
        {
            TypeAdapterConfig<TblMessage, LastMessageDto>.NewConfig()
                .Map(dst => dst.Body, src => src.Body)
                .Map(dst => dst.Time, src => src.CreatedDate.ToLongDateString())
                ;
        }
    }
}

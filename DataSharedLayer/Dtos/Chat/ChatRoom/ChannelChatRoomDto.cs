using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Domain.Profiles;
using DomainShared.Extentions.MapExtentions;
using DomainShared.Services;

namespace DomainShared.Dtos.Chat.ChatRoom
{
    public class ChannelChatRoomDto : IHasCustomMap
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public string MyChatId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Pic { get; set; }

        public string NavbarText { get; set; }
        public ChatRoomType Type { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblChatRoom, ChannelChatRoomDto>.NewConfig()
                .Map(dest => dest.Name, src => src.ChatRoomTitle)
                .Map(dest => dest.Pic, src => NavigationProfile.Resources + src.ProfileImage.Url);
        }

    }
}

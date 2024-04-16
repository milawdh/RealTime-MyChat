using Domain.Enums;
using Domain.Entities;
using Domain.Profiles;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Extentions.MapExtentions;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.ChatRoom
{
    public class GroupChatRoomDto : IHasCustomMap
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
            TypeAdapterConfig<TblChatRoom, GroupChatRoomDto>.NewConfig()
                .Map(dest => dest.Name, src => src.ChatRoomTitle)
                .Map(dest => dest.Pic, src => NavigationProfile.Resources + src.ProfileImage.Url)
                .Map(dest => dest.NavbarText, src => src.GetNavbarText());
        }


    }
}

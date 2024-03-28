using Domain.Enums;
using Domain.Models;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Profiles;
using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Chat.ChatRoom
{
    public class InitChatRoom : IHasCustomMap
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }

        public int NotSeenMessagesCount { get; set; } = 0;
        public LastMessageDto LastMessage { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblChatRoom, InitChatRoom>.NewConfig()
                .Map(dest => dest.Name, src => src.Type == ChatRoomType.Group ? src.ChatRoomTitle :
                src.TblUserChatRoomRel.First().User.Name)
                .Map(dest => dest.Pic, src => src.Type == ChatRoomType.Group ?
                NavigationProfile.Resources + src.ProfileImage.Url :
                NavigationProfile.Resources + src.TblUserChatRoomRel.First().User.ProfileImageUrlNavigation.Url)
                .Map(dest => dest.LastMessage, src => src.TblMessage.OrderBy(x => x.SendAt).LastOrDefault().Adapt<LastMessageDto>());
        }
    }
}

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
    public class PrivateChatRoomDto : IHasCustomMap
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public string NavbarText { get; set; }
        public ChatRoomType Type { get; set; }

        public List<MessagesDto> Messages { get; set; }

        public void ConfigMap()
        {
            TypeAdapterConfig<TblChatRoom, PrivateChatRoomDto>.NewConfig()
                .Map(i => i.NavbarText, src => src.GetNavbarText())
                .Map(i => i.Pic, src => NavigationProfile.Resources + src.TblUserChatRoomRel.FirstOrDefault()!.User.ProfileImageUrlNavigation.Url)
                .Map(dest => dest.Messages, src => GetMessages(src))
                .Map(i => i.Name, src => src.TblUserChatRoomRel.FirstOrDefault()!.User.Name);
        }

        private List<MessagesDto> GetMessages(TblChatRoom src) =>
         src.TblMessage.OrderBy(x => x.SendAt).AsQueryable().ProjectToType<MessagesDto>().ToList();

    }
}

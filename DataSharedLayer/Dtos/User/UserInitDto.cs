using Domain.Enums;
using Domain.Entities;
using Domain.Profiles;
using DomainShared.Dtos.Chat.ChatRoom;

using DomainShared.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.User
{
    public class UserInitDto : IHasCustomMap
    {
        //Current User Info
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public string Number { get; set; }
        public string? Bio { get; set; }

        /// <summary>
        /// Current User ChatRooms
        /// </summary>
        public List<InitChatRoom> ChatRooms { get; set; } = new();

        #region Maps

        public void ConfigMap()
        {
            //User Info
            TypeAdapterConfig<TblUser, UserInitDto>.NewConfig()
                .Map(dest => dest.Pic, src => NavigationProfile.Resources + src.ProfileImageUrlNavigation.Url)
                .Map(dest => dest.Number, src => src.Tell)
                .Map(dest => dest.Name, src => src.UserName);

        }

        #endregion
    }
}

﻿using Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace DomainShared.Services
{
    public static class DtoExtentions
    {
        /// <summary>
        /// Gets text That Will be shown on Navbar for User From ChatRoom
        /// </summary>
        /// <param name="chatRoom">Chat Room That User opened</param>
        /// <returns></returns>
        public static string GetNavbarText(this TblChatRoom chatRoom)
        {
            string result = "";
            switch (chatRoom.Type)
            {
                case Domain.Enums.ChatRoomType.Private:
                    var user = chatRoom.TblUserChatRoomRel.FirstOrDefault()!.User;
                    if (user.IsOnline)
                        result = "Online";
                    else
                    {
                        if ((DateTime.Now - user.LastOnline).Days > 1)
                            result = user.LastOnline.ToString("yyyy/M/dd H:m");
                        else
                            result = user.LastOnline.ToString("H:m");
                    }
                    break;
                case Domain.Enums.ChatRoomType.Group:
                    var userNames = chatRoom.TblUserChatRoomRel.Select(i => i.User.UserName).Take(3).ToList();
                    result = "You, " + String.Join(", ", userNames);
                    if (chatRoom.TblUserChatRoomRel.Count > 2)
                        result += "...";
                    break;
                case Domain.Enums.ChatRoomType.Channel:
                    break;
                case Domain.Enums.ChatRoomType.Community:
                    break;
                case Domain.Enums.ChatRoomType.SecretChat:
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
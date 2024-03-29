using Domain.Enums;
using Domain.Models;
using DomainShared.Base;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Profiles;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.API;
using ServiceLayer.Extentions;
using ServiceLayer.Hubs;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Services.User;
using Services.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Chat
{
    public interface IChatServices
    {
        Task<ServiceResult<MessagesDto>> SendMessageAsync(SendMessageDto messageDto);
        Task RecieveMessageAsync(MessagesDto message);
        Task<ServiceResult<object>> GetChatRoomAsync(Guid id);

    }

    public class ChatService : IChatServices
    {
        #region Constructor

        private readonly IUserInfoContext _userInfoContext;
        private readonly Core _core;
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub, IChatHubApi> _chatHub;

        public ChatService(IUserService userService,
            IUserInfoContext userInfoContext,
            Core core,
            IHubContext<ChatHub, IChatHubApi> chatHub
            )
        {
            _userService = userService;
            _userInfoContext = userInfoContext;
            _core = core;
            _chatHub = chatHub;
        }

        #endregion

        public async Task<ServiceResult<MessagesDto>> SendMessageAsync(SendMessageDto messageDto)
        {
            if (!_core.TblUserChatRoomRel.Any(x => x.ChatRoomId == messageDto.RecieverChatRoomId && x.UserId == _userInfoContext.UserId))
                return new ServiceResult<MessagesDto>("Invalid ChatRoom!");

            TblMessage tblMessage = new()
            {
                Body = messageDto.Body,
                RecieverChatRoomId = messageDto.RecieverChatRoomId,
                SenderUserId = _userInfoContext.UserId
            };

            _core.TblMessage.Add(tblMessage);
            _core.Save();

            var result = tblMessage.Adapt<MessagesDto>();
            //TODO : Do it with backTask

            await RecieveMessageAsync(result);
            return new ServiceResult<MessagesDto>(result);
        }

        /// <summary>
        /// Gets a Private ChatRoom With Messages From Current User's ChatRoom
        /// </summary>
        /// <param name="id">ChatRoom Id</param>
        /// <returns></returns>
        public async Task<ServiceResult<PrivateChatRoomDto>> GetPrivateChatRoomAsync(Guid id)
        {
            var chatRoom = _userInfoContext.ChatRoomsWithMessages
                .Where(x => x.Type == ChatRoomType.Private)
                .FirstOrDefault(i => i.Id == id);

            if (chatRoom is null)
                return new ServiceResult<PrivateChatRoomDto>("Chat Room Not Found");

            chatRoom.TblUserChatRoomRel = chatRoom.TblUserChatRoomRel.OrderByDescending(x => x.UserId != _userInfoContext.UserId).ToList();
            return new ServiceResult<PrivateChatRoomDto>(chatRoom.Adapt<PrivateChatRoomDto>());
        }

        /// <summary>
        /// Gets a Group ChatRoom With Messages From Current User's ChatRoom
        /// </summary>
        /// <param name="id">ChatRoom Id</param>
        /// <returns></returns>
        public async Task<ServiceResult<GroupChatRoomDto>> GetGroupChatRoomAsync(Guid id)
        {
            var chatRoom = _userInfoContext.ChatRoomsWithMessages
                .Where(x => x.Type == ChatRoomType.Group)
                .FirstOrDefault(i => i.Id == id);

            if (chatRoom is null)
                return new ServiceResult<GroupChatRoomDto>("Chat Room Not Found");

            return new ServiceResult<GroupChatRoomDto>(chatRoom.Adapt<GroupChatRoomDto>());
        }

        /// <summary>
        /// Calls RecieveMessage Method In Message's Reciever Chat Room Members's Scope from ChatHub Api
        /// </summary>
        /// <param name="messageDto">Message Dto Will Send</param>
        /// <returns>It Sends data from ChatHub</returns>
        public async Task RecieveMessageAsync(MessagesDto messageDto)
        {
            var userNames = _userInfoContext.ChatRooms
                .FirstOrDefault(i => i.Id == messageDto.RecieverChatRoomId)!
                .GetChatRoomUserNames(_userInfoContext.UserId);


            var result = messageDto.Adapt<RecieveMessageDto>();
            result.SenderChatRoom = ChatRoomExtentions.GetInitChatRoom(_userInfoContext.UserId,
                _userInfoContext.ChatRoomsWithMessages.FirstOrDefault(i => i.Id == messageDto.RecieverChatRoomId));

            await _chatHub.Clients.Users(userNames).RecieveMessage(new ApiResult<RecieveMessageDto>(new ServiceResult<RecieveMessageDto>(result)));
        }

        /// <summary>
        /// Gets ChatRoom With Messages And Sets All Messages Readed By CurrentUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<object>> GetChatRoomAsync(Guid id)
        {
            if (!_core.TblChatRoom.Any(x => x.Id == id))
                return new ServiceResult<object>("ChatRoomNot Found");

            await SetMessageRead(id);

            var chatRoomType = _core.TblChatRoom.GetById(id).Type;
            switch (chatRoomType)
            {
                case ChatRoomType.Private:
                    return (await GetPrivateChatRoomAsync(id)).ToObjectiveServiceResult();
                case ChatRoomType.Group:
                    return (await GetGroupChatRoomAsync(id)).ToObjectiveServiceResult();
                case ChatRoomType.Channel:
                    break;
                case ChatRoomType.Community:
                    break;
                case ChatRoomType.SecretChat:
                    break;
                default:
                    break;
            }

            return new ServiceResult<object>("ChatRoom Not Found!");
        }

        /// <summary>
        /// Sets Specified MessageReaded By Current User
        /// </summary>
        /// <param name="chatRoomId">Message's ChatRoom Id</param>
        /// <param name="messageId">Message Should Set Read</param>
        /// <returns></returns>
        public async Task SetMessageRead(Guid chatRoomId, Guid messageId)
        {
            _core.TblUserChatRoomRel.Get(i => i.UserId == _userInfoContext.UserId && i.ChatRoomId == chatRoomId)
                .FirstOrDefault().LastSeenMessageId = messageId;
            _core.Save();
        }

        /// <summary>
        /// Sets ChatRoom Last Message ReadedBy CurrentUser
        /// </summary>
        /// <param name="chatRoomId">ChatRoom's Id</param>
        /// <returns></returns>
        public async Task SetMessageRead(Guid chatRoomId)
        {

            var map = _core.TblUserChatRoomRel.Get(i => i.UserId == _userInfoContext.UserId && i.ChatRoomId == chatRoomId,
                includes: x => x.Include(c => c.ChatRoom).ThenInclude(v => v.TblMessage))
                 .FirstOrDefault();

            map.LastSeenMessageId = map?.ChatRoom?.TblMessage?.OrderBy(x => x.SendAt).LastOrDefault()?.Id;
            _core.Save();
        }
    }
}

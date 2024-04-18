using Domain.API;
using Domain.Base;
using Domain.Enums;
using Domain.Entities;
using DomainShared.Dtos.Chat.ChatRoom;
using DomainShared.Dtos.Chat.Message;
using DomainShared.Extentions.Utility;
using DomainShared.Extentions.MapExtentions;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Hubs;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Services.User;
using Domain.DataLayer.UnitOfWorks;
using Domain.DataLayer.Repository;
using ServiceLayer.Services.File;

namespace ServiceLayer.Services.Chat
{
    public interface IChatServices
    {
        Task<ServiceResult<MessagesDto>> SendMessageAsync(SendMessageDto messageDto);
        Task RecieveMessageAsync(TblMessage message);
        Task<ServiceResult<object>> GetChatRoomAsync(Guid id);
        Task SetMessageReadAsync(Guid chatRoomId, Guid messageId, Guid userId);
        Task SetChatRoomAllMessagesReadAsync(Guid chatRoomId);
        Task<ServiceResult<List<MessagesDto>>> GetChatRoomMessagesAsync(Guid chatRoomId, int startRow);

    }
    public class ChatService : IChatServices
    {
        #region Constructor

        private readonly IUserInfoContext _userInfoContext;
        private readonly Core _core;
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub, IChatHubApi> _chatHub;
        private readonly IChatHubGroupManager _chatHubGroupManager;
        private readonly IMediaServcie _mediaServcie;

        public ChatService(IUserService userService,
            IUserInfoContext userInfoContext,
            Core core,
            IHubContext<ChatHub, IChatHubApi> chatHub,
            IChatHubGroupManager chatHubGroupManager,
            IMediaServcie mediaServcie)
        {
            _userService = userService;
            _userInfoContext = userInfoContext;
            _core = core;
            _chatHub = chatHub;
            _chatHubGroupManager = chatHubGroupManager;
            _mediaServcie = mediaServcie;
        }

        #endregion

        public async Task<ServiceResult<MessagesDto>> SendMessageAsync(SendMessageDto messageDto)
        {
            if (!_userInfoContext.ChatRooms.Any(x => x.Id == messageDto.RecieverChatRoomId))
                return new ServiceResult<MessagesDto>("Invalid ChatRoom!");

            _core.BeginTransaction();
            try
            {

                TblMessage tblMessage = new()
                {
                    Body = messageDto.Body,
                    RecieverChatRoomId = messageDto.RecieverChatRoomId,
                    CreatedById = _userInfoContext.UserId,
                    CreatedBy = _userInfoContext.User,
                };

                _core.TblMessage.Add(tblMessage);

                if (messageDto.File != null)
                    _mediaServcie.Add(new DomainShared.Dtos.Media.CreateUpdateMediaDto { Message = tblMessage, File = messageDto.File });

                _core.Save();

                await SetMessageReadAsync(tblMessage.RecieverChatRoomId, tblMessage.Id, _userInfoContext.UserId);

                MessagesDto result = tblMessage.Adapt<MessagesDto>();

                //TODO : Do it with backTask
                await RecieveMessageAsync(tblMessage);

                _core.CommitTransaction();
                return new ServiceResult<MessagesDto>(result);
            }
            catch (Exception ex)
            {
                _core.RollBackTransaction();
                ElmahCore.ElmahExtensions.RaiseError(ex);

                return new ServiceResult<MessagesDto>("An Error Occured in sending Message!");
            }
        }

        public async Task<ServiceResult<List<MessagesDto>>> GetChatRoomMessagesAsync(Guid chatRoomId, int startRow)
        {
            if (!_userInfoContext.ChatRooms.Any(x => x.Id == chatRoomId))
                return new ServiceResult<List<MessagesDto>>("ChatRoom Not Found!");

            return new ServiceResult<List<MessagesDto>>(chatRoomId.GetChatRoomMessageDtos(_core, _userInfoContext.UserId, startRow));
        }

        /// <summary>
        /// Calls RecieveMessage Method In Message's Reciever Chat Room Members's Scope from ChatHub Api
        /// </summary>
        /// <param name="messageDto">Message Dto Will Send</param>
        /// <returns>It Sends data from ChatHub</returns>
        public async Task RecieveMessageAsync(TblMessage tblMessage)
        {
            Dictionary<string, string> dict = _chatHubGroupManager.GetUsersCurrentChatRoomDict();

            var allRecieverUsers = _userInfoContext.ChatRooms.Where(i => i.Id == tblMessage.RecieverChatRoomId)
                .SelectMany(v => v.TblUserChatRoomRels.Select(c => c.User.ConnectionId)).Where(a => a != null).ToList();


            var willNotifyUsers = allRecieverUsers.Where(c => !dict.ContainsKey(c) || dict[c] != tblMessage.RecieverChatRoomId.ToString())
                .ToList();

            var notification = tblMessage.MapToRecieveMessageNotificationDto(_core);

            var result = tblMessage.Adapt<RecieveMessageDto>();

            await _chatHub.Clients.GroupExcept(tblMessage.RecieverChatRoomId.ToString(), _userInfoContext.User.ConnectionId)
                .RecieveMessage(new ApiResult<RecieveMessageDto>(new ServiceResult<RecieveMessageDto>(result)));

            await _chatHub.Clients.Clients(willNotifyUsers)
                .RecieveNotification(new ApiResult<RecieveMessageNotificationDto>(new ServiceResult<RecieveMessageNotificationDto>(notification)));
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

            var chatRoom = _userInfoContext.ChatRooms.FirstOrDefault(x => x.Id == id);

            await SetChatRoomAllMessagesReadAsync(id);

            var chatRoomType = chatRoom.Type;

            switch (chatRoomType)
            {
                case ChatRoomType.Private:
                    return new ServiceResult<object>(chatRoom.MapToPrivateChatRoomDto(_userInfoContext.UserId));
                case ChatRoomType.Group:
                    return new ServiceResult<object>(chatRoom.MapToGroupChatRoomDto(_userInfoContext.UserId));
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
        /// Sets Specified MessageReaded By Specified User
        /// </summary>
        /// <param name="chatRoomId">Message's ChatRoom Id</param>
        /// <param name="messageId">Message Should Set Read</param>
        /// <param name="userId">Specified User's Id</param>
        /// <returns></returns>
        public async Task SetMessageReadAsync(Guid chatRoomId, Guid messageId, Guid userId)
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
        public async Task SetChatRoomAllMessagesReadAsync(Guid chatRoomId)
        {

            var map = _core.TblUserChatRoomRel.Get(i => i.UserId == _userInfoContext.UserId && i.ChatRoomId == chatRoomId)
                .Include(c => c.ChatRoom).ThenInclude(x => x.TblMessages.OrderByDescending(c => c.CreatedDate).Take(1))
                .AsSplitQuery()
                 .FirstOrDefault();

            map.LastSeenMessageId = map?.ChatRoom?.TblMessages?.OrderBy(x => x.CreatedDate).FirstOrDefault()?.Id;
            _core.Save();

            await _chatHub.Clients.GroupExcept(chatRoomId.ToString(), _userInfoContext.UserChatHubConnectionId).SetAllMessagesRead();
        }
    }
}

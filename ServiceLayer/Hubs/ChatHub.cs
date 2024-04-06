using Domain.Base;
using DomainShared.Dtos.Chat.Message;
using ElmahCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Services.Chat;
using ServiceLayer.Services.User;

namespace ServiceLayer.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHubApi>
    {
        private readonly IUserInfoContext _userInfoContext;
        private readonly IUserService _userService;
        private readonly IChatServices _chatServices;
        private readonly IChatHubGroupManager _chatHubGroupManager;

        public ChatHub(IUserInfoContext webAppContext, IUserService userService,
            IChatServices chatServices,
            IChatHubGroupManager chatHubGroupManager)
        {
            _userInfoContext = webAppContext;
            _userService = userService;
            _chatServices = chatServices;
            _chatHubGroupManager = chatHubGroupManager;
        }

        public override async Task OnConnectedAsync()
        {
            //Set UserOnline
            _userService.SetUserOnline(Context.ConnectionId);
            await Clients.Caller.SetUserInfo(_userInfoContext.UserInitiliazeDto);


            await base.OnConnectedAsync();
            //Get User Data
            await Clients.Caller.GetCurrentChatRoom();
        }

        #region Chat

        /// <summary>
        /// Sets User CurrentChatRoom To GroupManagement
        /// </summary>
        /// <param name="chatRoomId"></param>
        /// <returns></returns>
        public async Task SetCurrentChatRoom(Guid? chatRoomId)
        {
            if (chatRoomId != null)
                _chatHubGroupManager.SetCurrentChatRoom(Context.ConnectionId, chatRoomId.Value.ToString());
        }

        /// <summary>
        /// Gets ChatRoom From Current User's ChatRooms With Messages To Converstation
        /// </summary>
        /// <param name="id">ChatRoom Id</param>
        /// <returns>Dependent On ChatRoom type ChatRoom's ViewModel</returns>
        public async Task<object> GetChatRoomDetails(Guid id)
        {
            var result = await _chatServices.GetChatRoomAsync(id);

            return new ApiResult<object>(result);
        }

        /// <summary>
        /// Sends Message To User CurrentChatRoom That Setted in GroupManager
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<ApiResult<MessagesDto>> SendMessage(string body)
        {
            var accessResult = _chatHubGroupManager.GetCurrentChatRoom(Context.ConnectionId);
            if (accessResult.Failure)
                return new ApiResult<MessagesDto>(accessResult.Messages);

            //Context.c
            return new ApiResult<MessagesDto>(
                await _chatServices.SendMessageAsync(
                new SendMessageDto { Body = body, RecieverChatRoomId = new Guid(accessResult.Result) })
                );
        }

        public async Task SetMessageRead(Guid chatRoomId)
        {
            if (_userInfoContext.ChatRooms.Any(x => x.Id == chatRoomId))
                await _chatServices.SetChatRoomAllMessagesRead(chatRoomId);
        }

        #endregion

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //Set User Offline
            _userService.SetUserOffline();
            _chatHubGroupManager.SetDisconnected(Context.ConnectionId);

            if (exception != null)
                ElmahExtensions.RaiseError(exception);

            await base.OnDisconnectedAsync(exception);
        }
    }
}

using DomainShared.Base;
using DomainShared.Dtos.Chat.Message;
using ElmahCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Services.Chat;
using ServiceLayer.Services.User;

namespace ServiceLayer.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHubApi>
    {
        private static Dictionary<Guid, string> UsersChatRooms = new Dictionary<Guid, string>();

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
            try
            {
                //Set UserOnline
                _userService.SetUserOnline();
                await Clients.Caller.SetUserInfo(_userInfoContext.UserInitiliazeDto);


                await base.OnConnectedAsync();
                //Get User Data
                await Clients.Caller.GetCurrentChatRoom();
            }
            catch (Exception ex)
            {
                ElmahExtensions.RaiseError(ex);
            }
        }

        public async Task SetCurrentChatRoom(Guid? chatRoomId)
        {
            if (chatRoomId != null)
                _chatHubGroupManager.SetCurrentChatRoom(Context.ConnectionId, chatRoomId.Value.ToString());
        }


        #region Chat

        /// <summary>
        /// Gets ChatRoom From Current User's ChatRooms With Messages To Converstation
        /// </summary>
        /// <param name="id">ChatRoom Id</param>
        /// <returns>Dependent On ChatRoom type ChatRoom's ViewModel</returns>
        public async Task<object> GetChatRoomDetials(Guid id)
        {
            try
            {
                var result = await _chatServices.GetChatRoomAsync(id);

                return new ApiResult<object>(result);
            }
            catch (Exception ex)
            {
                ElmahExtensions.RaiseError(ex);
                return new ApiResult<object>("Couldn't Get Chatroom!");
            }
        }

        public async Task<ApiResult<MessagesDto>> SendMessage(string body)
        {
            try
            {
                var accessResult = _chatHubGroupManager.GetCurrentChatRoom(Context.ConnectionId);
                if (accessResult.Failure)
                    return new ApiResult<MessagesDto>(accessResult.Messages);

                return new ApiResult<MessagesDto>(
                    await _chatServices.SendMessageAsync(
                    new SendMessageDto { Body = body, RecieverChatRoomId = new Guid(accessResult.Result) })
                    );
            }
            catch (Exception ex)
            {
                ElmahExtensions.RaiseError(ex);
                return new ApiResult<MessagesDto>("Message Not Sent");
            }
        }

        #endregion

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //Set User Offline
            _userService.SetUserOffline();
            _chatHubGroupManager.SetDisconnected(Context.ConnectionId);


            ElmahExtensions.RaiseError(exception);
            await base.OnDisconnectedAsync(exception);
        }
    }
}

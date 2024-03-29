using DomainShared.Base;
using DomainShared.Dtos.Chat.Message;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Services.User;
using ElmahCore;
using Microsoft.AspNetCore.Authorization;
using ServiceLayer.Services.Chat;

namespace ServiceLayer.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHubApi>
    {
        private static Dictionary<Guid, string> UsersChatRooms = new Dictionary<Guid, string>();

        private readonly IUserInfoContext _userInfoContext;
        private readonly IUserService _userService;
        private readonly IChatServices _chatServices;

        public ChatHub(IUserInfoContext webAppContext, IUserService userService, IChatServices chatServices)
        {
            _userInfoContext = webAppContext;
            _userService = userService;
            _chatServices = chatServices;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                

                _userService.SetUserOnline();
                await Clients.Caller.SetUserInfo(_userInfoContext.UserInitiliazeDto);


                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                ElmahExtensions.RaiseError(ex);
            }
        }

        #region Chat

        /// <summary>
        /// Gets ChatRoom From Current User's ChatRooms With Messages To Converstation
        /// </summary>
        /// <param name="id">ChatRoom Id</param>
        /// <returns>Dependent On ChatRoom type ChatRoom's ViewModel</returns>
        public async Task<object> GetChatRoom(Guid id)
        {
            try
            {

                var result = await _chatServices.GetChatRoomAsync(id);
                //if (result.Success)
                    //SetUserCurrentRoom(id.ToString());

                return new ApiResult<object>(result);
            }
            catch (Exception ex)
            {
                ElmahExtensions.RaiseError(ex);
                return new ApiResult<object>("Couldn't Get Chatroom!");
            }
        }

        public async Task<ApiResult<MessagesDto>> SendMessage(SendMessageDto sendMessageDto)
        {
            try
            {
                return new ApiResult<MessagesDto>(await _chatServices.SendMessageAsync(sendMessageDto));
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
            _userService.SetUserOffline();

            ElmahExtensions.RaiseError(exception);
            await base.OnDisconnectedAsync(exception);
        }
    }
}

using Domain.Base;
using Domain.Entities;

using DomainShared.Dtos.Chat.Message;
using DomainShared.Dtos.User;

namespace ServiceLayer.Hubs.Api
{
    public interface IChatHubApi
    {
        Task SetUserInfo(UserInitDto userDto);
        Task AddMessage(ApiResult<MessagesDto> message);
        Task RecieveNotification(ApiResult<RecieveMessageNotificationDto> message);

        Task GetCurrentChatRoom();
        Task SetAllMessagesRead();

        Task ShowError(List<string> message);
        Task ShowError(params string[] message);
    }
}

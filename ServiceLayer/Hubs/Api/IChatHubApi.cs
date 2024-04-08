using Domain.Base;
using Domain.Entities;

using DomainShared.Dtos.Chat.Message;
using DomainShared.Dtos.User;

namespace ServiceLayer.Hubs.Api
{
    public interface IChatHubApi
    {
        Task SetUserInfo(UserInitDto userDto);
        Task RecieveMessage(ApiResult<RecieveMessageDto> message);
        Task RecieveNotification(ApiResult<RecieveMessageNotificationDto> message);

        Task GetCurrentChatRoom();
        Task SetAllMessagesRead();
    }
}

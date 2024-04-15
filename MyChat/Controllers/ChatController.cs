using Domain.Base;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using DomainShared.Dtos;
using DomainShared.Dtos.Chat.Message;
using Framework.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Chat;
using ServiceLayer.Services.File;
using ServiceLayer.Services.User;

namespace MyChat.Controllers
{
    [Authorize]
    public class ChatController : CustomBaseApiController
    {
        private readonly IUserInfoContext _userInfoContext;
        private readonly IChatServices _chatServices;
        private readonly IChatHubGroupManager _chatHubGroupManager;
        private readonly IFileService _fileService;
        private readonly Core _core;

        public ChatController(IUserInfoContext webAppContext, IChatServices chatServices, IChatHubGroupManager chatHubGroupManager, Core core, IFileService fileService)
        {
            _userInfoContext = webAppContext;
            _chatServices = chatServices;
            _chatHubGroupManager = chatHubGroupManager;
            this._core = core;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View(_userInfoContext.User);
        }

        public async Task<IActionResult> DownloadMedia(Guid id)
        {
            var media = _core.TblMedia.FirstOrDefualt(x => x.Id == id);
            if (media == null)
                return BadResult("Media Doesn't Exist");

            var data = _fileService.Get(media);
            if (data.Failure)
                return BadResult("Media Doesn't Exist");

            return File(data.Result, media.FileMimType);
        }

        public async Task<IActionResult> SendMessageWithFile(string? messageBody, IFormFile file)
        {

            var currentChatRoom = _chatHubGroupManager.GetCurrentChatRoom(_userInfoContext.UserChatHubConnectionId);
            if (currentChatRoom.Failure)
                return BadResult(currentChatRoom.Messages);

            return SmartResult(await _chatServices.SendMessageAsync(new SendMessageDto
            {
                File = file,
                Body = messageBody,
                RecieverChatRoomId = new Guid(currentChatRoom.Result)
            }));
        }
    }
}

﻿using Domain.Models;
using DomainShared.Dtos;
using Framework.Api;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.API;
using ServiceLayer.Services.User;

namespace MyChat.Controllers
{
    [Authorize]
    public class ChatController : CustomBaseApiController
    {
        private readonly IUserInfoContext _userInfoContext;

        public ChatController(IUserInfoContext webAppContext)
        {
            _userInfoContext = webAppContext;
        }

        public IActionResult Index()
        {
            return View(_userInfoContext.User);
        }
	}
}
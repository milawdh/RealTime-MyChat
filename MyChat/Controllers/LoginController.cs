using Domain.Models;
using DomainShared.Base;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using ServiceLayer.Extentions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Mapster;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using ServiceLayer.API;
using DomainShared.Dtos.User;
using Framework.Api;
using ServiceLayer.Services.User;
using ElmahCore;

namespace MyChat.Controllers
{
    public class LoginController : CustomBaseApiController
    {
        private readonly IUserLoginService _userLoginService;

        public LoginController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Chat");
            }

            return View();
        }

        public IActionResult Register()
        {

            return View();
        }



        //Actions
        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult LoginAction(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadResult(ModelState);

            var loginResult = _userLoginService.Login(loginDto);

            return SignIn(loginResult.Claims, loginResult.AuthenticateProperties, loginResult.AuthenticationScheme);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult RegisterAction(UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadResult(ModelState);

            var registerResult = _userLoginService.Register(registerDto);

            return SignIn(registerResult.Claims, registerResult.AuthenticateProperties, registerResult.AuthenticationScheme);
        }


        public IActionResult Logout()
        {
            return SignOut(_userLoginService.SetUserLogOut());
        }
    }
}

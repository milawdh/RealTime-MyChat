using Microsoft.AspNetCore.Mvc;
using DomainShared.Dtos.User;
using Framework.Api;
using ServiceLayer.Services.User;
using Domain.DataLayer.UnitOfWorks;

namespace MyChat.Controllers
{
    public class LoginController : CustomBaseApiController
    {
        private readonly IUserLoginService _userLoginService;
        private readonly Core core;

        public LoginController(IUserLoginService userLoginService, Core core)
        {
            _userLoginService = userLoginService;
            this.core = core;
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

        public IActionResult Test(Guid id)
        {
            return Ok(core.TblMessage.Reomve(core.TblMessage.FirstOrDefualt(x => x.Id == id)));
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

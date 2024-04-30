using Domain.CustomExceptions;
using Domain.Entities;
using DomainShared.Dtos.User;
using DomainShared.Extentions.Utility;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain.DataLayer.UnitOfWorks;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Domain.DataLayer.Repository;

namespace ServiceLayer.Services.User
{
    public interface IUserLoginService
    {
        (ClaimsPrincipal Claims, AuthenticationProperties AuthenticateProperties, string AuthenticationScheme) Login(UserLoginDto loginDto);
        (ClaimsPrincipal Claims, AuthenticationProperties AuthenticateProperties, string AuthenticationScheme) Register(UserRegisterDto registerDto);
        AuthenticationProperties SetUserLogOut();
    }

    public class UserLoginService : IUserLoginService
    {
        #region Constructor
        
        private readonly Core _core;
        private readonly IUserInfoContext _webAppContext;
        public UserLoginService(Core core, IUserInfoContext webAppContext)
        {
            _core = core;
            _webAppContext = webAppContext;
        }

        #endregion

        #region Main Methods

        /// <summary>
        /// Logins User And Returns Principals needed for UserLogin
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>Principals needed for Signin Action</returns>
        public (ClaimsPrincipal Claims, AuthenticationProperties AuthenticateProperties, string AuthenticationScheme) Login(UserLoginDto loginDto)
        {
            loginDto.UserName = loginDto.UserName.ToLower();
            loginDto.Password = loginDto.Password.HashData();

            ValidateUserLogin(loginDto);

            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,loginDto.UserName)
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/Chat"
            };
            return (new ClaimsPrincipal(identity), properties, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Registers User And Returns Principals needed for User Register
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Principals needed for Signin Action</returns>
        public (ClaimsPrincipal Claims, AuthenticationProperties AuthenticateProperties, string AuthenticationScheme) Register(UserRegisterDto registerDto)
        {

            ValidateUserRegister(registerDto);

            TblUser user = registerDto.Adapt<TblUser>();
            user.Password = user.Password.HashData();
            user.UserName = user.UserName.ToLower();
            _core.TblUsers.Add(user);
            _core.TblMyChatIdentifier.Add(new TblMyChatIdentifier() { Identifier = registerDto.UserName });
            _core.Save();
            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserName)
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/Chat"
            };

            return (new ClaimsPrincipal(identity), properties, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public AuthenticationProperties SetUserLogOut()
        {
            return new AuthenticationProperties()
            {
                RedirectUri = "/Chat"
            };
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates User When Loging In
        /// </summary>
        /// <param name="loginDto"></param>
        /// <exception cref="AuthorizationException">Throws Exception on Invalid Dto</exception>
        /// 
        private void ValidateUserLogin(UserLoginDto loginDto)
        {
            if (!_core.TblUsers.Any(_ => _.UserName == loginDto.UserName && _.Password == loginDto.Password))
                throw new AuthorizationException("UserName Or Password Is wrong");
        }

        /// <summary>
        /// Validates User When Signing In
        /// </summary>
        /// <param name="registerDto"></param>
        /// <exception cref="AuthorizationException">Throws Exception on Invalid Dto</exception>
        private void ValidateUserRegister(UserRegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.PasswordRepeat)
                throw new AuthorizationException("Password Repeat doesn't Match!");

            if (_core.TblUsers.Any(_ => _.Tell == registerDto.Tell))
                throw new AuthorizationException("This Phone Number Exist!");

            var regex = new Regex("^\\d[-0-9]+\\d$");
            if (registerDto.Tell.Length != 11 || !regex.IsMatch(registerDto.Tell))
                throw new AuthorizationException("Invalid Phone Number!");

            if (_core.TblMyChatIdentifier.Any(_ => _.Identifier == registerDto.UserName))
                throw new AuthorizationException("This UserName Allready Exist!");
        }

        #endregion
    }
}

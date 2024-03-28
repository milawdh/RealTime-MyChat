using Domain.Models;
using DomainShared.Base;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.API;
using ServiceLayer.CustomExceptions;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Hubs;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.User
{
    public interface IUserService
    {
        //TblUsers GetUserByUserName(string userName, Func<IQueryable<TblUsers>, IQueryable<TblUsers>> include = null);
        void SetUserOffline();
        void SetUserOnline();
    }
    public class UserService : IUserService
    {
        #region Constructor

        private readonly Core _core;
        private readonly IUserInfoContext _userInfoContext;
        private readonly IHubContext<ChatHub, IChatHubApi> _chatHub;
        public UserService(Core core, IHubContext<ChatHub, IChatHubApi> chatHub,
            IUserInfoContext userInfoContext)
        {
            _core = core;
            _chatHub = chatHub;
            _userInfoContext = userInfoContext;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets User Model By It's Identifier
        /// </summary>
        /// <param name="userName">User's UserName</param>
        /// <param name="include">Include Parameters</param>
        /// <returns></returns>
        /// <exception cref="AuthenticateException">occurs when userName was invalid</exception>
        //public TblUsers GetUserByUserName(string userName, Func<IQueryable<TblUsers>, IQueryable<TblUsers>> include = null)
        //{
        //    if (!Query.Any(i => i.UserName == userName))
        //        throw new AuthenticateException("UserName Not Found");
        //    var query = Query;
        //    if (include != null)
        //        query = include(Query);
        //    TblUsers result = query.FirstOrDefault(i => i.UserName == userName)!;
        //    return result;
        //}

        /// <summary>
        /// Sets User Online By Id
        /// </summary>
        /// <param name="userId">Users Id</param>
        /// <exception cref="AuthenticateException">occurs when userName was invalid</exception>
        public void SetUserOnline()
        {
            _userInfoContext.User.IsOnline = true;
            _core.Save();


        }

        /// <summary>
        /// Sets User Offline By Id
        /// </summary>
        /// <param name="userId">Users Id</param>
        /// <exception cref="AuthenticateException">occurs when userName was invalid</exception>
        public void SetUserOffline()
        {
            var user = _userInfoContext.User;

            user.IsOnline = false;
            user.LastOnline = DateTime.Now;
            _core.Save();


        }

        #endregion
    }
}

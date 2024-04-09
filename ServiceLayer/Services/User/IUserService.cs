using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Hubs.Api;
using ServiceLayer.Hubs;
using Domain.DataLayer.UnitOfWorks;
using ServiceLayer.Services.Caching;
using Domain.Profiles;
using Framework.CacheManagement;
using Domain.API;
using Domain.DataLayer.Repository;

namespace ServiceLayer.Services.User
{
    public interface IUserService
    {
        //TblUsers GetUserByUserName(string userName, Func<IQueryable<TblUsers>, IQueryable<TblUsers>> include = null);
        void SetUserOffline();
        void SetUserOnline(string connectionId);
    }
    public class UserService : IUserService
    {
        #region Constructor

        private readonly Core _core;
        private readonly IUserInfoContext _userInfoContext;
        private readonly ICacheManager _cache;
        private readonly IHubContext<ChatHub, IChatHubApi> _chatHub;
        public UserService(Core core, IHubContext<ChatHub, IChatHubApi> chatHub,
            IUserInfoContext userInfoContext, ICacheManager cache)
        {
            _core = core;
            _chatHub = chatHub;
            _userInfoContext = userInfoContext;
            _cache = cache;
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
        /// <param name="connectionId">User's connectionId Id</param>
        public void SetUserOnline(string connectionId)
        {
            var user = _userInfoContext.User;

            user.IsOnline = true;
            user.ConnectionId = connectionId;

            _core.Save();
        }

        /// <summary>
        /// Sets User Offline By Id
        /// </summary>
        public void SetUserOffline()
        {
            var user = _userInfoContext.User;

            user.IsOnline = false;
            user.LastOnline = DateTime.Now;
            user.ConnectionId = null;

            _core.Save();
        }

        #endregion
    }
}

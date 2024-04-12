using Domain.CustomExceptions;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Domain.DataLayer.UnitOfWorks;
using System.Security.Claims;
using Domain.Entities;
using Domain.DataLayer.Contexts;
using Domain.DataLayer.Contexts.Base;

namespace Domain.DataLayer.Repository
{
    public interface IUserInfoContext
    {
        Guid UserId { get; }
        string UserName { get; }
        string? UserChatHubConnectionId { get; }
        TblUser User { get; }
        IQueryable<TblPermission> UserPermissions { get; }

        /// <summary>
        /// Current User's ChatRooms With Their Messages
        /// </summary>
        /// <returns></returns>
        IQueryable<TblChatRoom> ChatRoomsWithMessages { get; }

        /// <summary>
        /// Current User's ChatRooms Without Their Messages
        /// </summary>
        IQueryable<TblChatRoom> ChatRooms { get; }

        /// <summary>
        /// Current User's Contacts
        /// </summary>
        IQueryable<TblUserContacts> UserContacts { get; }

        /// <summary>
        /// Users That Current User is their contact
        /// </summary>
        IQueryable<TblUserContacts> UserIntegratedContacts { get; }

        /// <summary>
        /// Gets Current User's data With Custom Query
        /// </summary>
        /// <param name="custom">Custom Query</param>
        /// <returns></returns>
        /// <exception cref="AuthenticateException">If UserName Was Not Found In Database It Occurs</exception>
        public IQueryable<TblUser> GetUser(Func<IQueryable<TblUser>, IQueryable<TblUser>> custom = null);
    }

    public class UserInfoContext : IUserInfoContext
    {
        #region Cunstructor

        private readonly HttpContext HttpContext;
        private readonly Core core;
        private static string? _userName;
        private readonly IQueryable<TblUser> tblUsers;
        private readonly IQueryable<TblUserContacts> tblUserContacts;
        private readonly IQueryable<TblChatRoom> chatRooms;
        private readonly IQueryable<TblUserChatRoomRel> tblUserChatRooms;

        public UserInfoContext(IHttpContextAccessor httpContextAccessor, Core core)
        {
            HttpContext = httpContextAccessor.HttpContext;
            tblUsers = core.TblUsers.Get();
            tblUserContacts = core.TblUserContacts.Get();
            chatRooms = core.TblChatRoom.Get();
            tblUserChatRooms = core.TblUserChatRoomRel.Get();
        }

        public UserInfoContext(Core core, string userName)
        {
            tblUsers = core.TblUsers.Get();
            tblUserContacts = core.TblUserContacts.Get();
            chatRooms = core.TblChatRoom.Get();
            tblUserChatRooms = core.TblUserChatRoomRel.Get();
            _userName = userName;
        }

        #endregion

        #region Consts

        /// <summary>
        /// Users ChatRooms DefualtIncludes
        /// </summary>
        Func<IQueryable<TblChatRoom>, IQueryable<TblChatRoom>> ChatRoomDefualtQuery
        {
            get
            {
                return
                i => i.Include(x => x.TblUserChatRoomRels.OrderByDescending(x => x.UserId != UserId)).ThenInclude(x => x.User);
            }
        }

        /// <summary>
        /// TblUser DefualtIncludes
        /// </summary>
        private Func<IQueryable<TblUser>, IQueryable<TblUser>> UserDefualtInclude
        {
            get
            {
                return (i) => i.Include(x => x.ProfileImageUrlNavigation);
            }
        }

        #endregion

        #region Properties

        #region Info

        /// <summary>
        /// Current User's Id
        /// </summary>
        public Guid UserId
        {
            get
            {

                return tblUsers.Where(i => i.UserName == UserName).Select(x => x.Id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Current User's UserName
        /// </summary>
        public string UserName
        {
            get
            {
                if (_userName is null)
                {
                    var userName = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!.Value;
                    if (!tblUsers.Any(i => i.UserName == userName))
                        throw new AuthenticateException("UserName Not Found");

                    _userName = userName;
                    return tblUsers.Where(i => i.Id == UserId).Select(x => x.UserName).FirstOrDefault();
                }
                return _userName;
            }
        }

        public string? UserChatHubConnectionId
        {
            get
            {
                return User.ConnectionId;
            }
        }

        /// <summary>
        /// Current User Main Model
        /// </summary>
        public TblUser User
        {
            get
            {
                return GetUser().FirstOrDefault();
            }
        }



        public IQueryable<TblUserContacts> UserContacts
        {
            get
            {
                return tblUserContacts.Where(x => x.CreatedById == UserId);
            }
        }

        public IQueryable<TblUserContacts> UserIntegratedContacts
        {
            get
            {
                return tblUserContacts.Where(x => x.ContactUserId == UserId);
            }
        }


        #endregion

        #region ChatRooms

        /// <summary>
        /// Current User's ChatRooms With Their Messages
        /// </summary>
        /// <returns></returns>
        public IQueryable<TblChatRoom> ChatRoomsWithMessages
        {
            get
            {
                return GetChatRooms(
                    i => i.Include(x => x.TblMessages
                    .Where(x => !x.IsDeleted)
                    .OrderBy(c => c.CreatedDate))
                    .ThenInclude(x => x.CreatedBy).ThenInclude(x => x.ProfileImageUrlNavigation)
                    .Include(x => x.TblMessages.Where(x => !x.IsDeleted).OrderBy(c => c.CreatedDate))
                    .Include(x => x.ProfileImage)
                    );
            }
        }

        /// <summary>
        /// Current User's ChatRooms Without Their Messages
        /// </summary>
        public IQueryable<TblChatRoom> ChatRooms
        {
            get
            {
                return GetChatRooms();
            }
        }

        #endregion

        #region Permissions

        /// <summary>
        /// Current User's Permissions
        /// </summary>
        public IQueryable<TblPermission> UserPermissions
        {
            get
            {
                TblRole tblRole = tblUsers.Where(i => i.UserName == UserName)
                    .Include(x => x.Role).ThenInclude(x => x.TblRolePermissionRels).ThenInclude(x => x.Permission)
                    .FirstOrDefault()!.Role;

                return tblRole.TblRolePermissionRels.Select(i => i.Permission).AsQueryable();

            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets Current User's data
        /// </summary>
        /// <param name="custom">Custom Query</param>
        /// <returns></returns>
        /// <exception cref="AuthenticateException">If UserName Was Not Found In Database It Occurs</exception>
        public IQueryable<TblUser> GetUser(Func<IQueryable<TblUser>, IQueryable<TblUser>> custom = null)
        {
            var query = tblUsers.Where(i => i.Id == UserId);
            query = UserDefualtInclude(query);
            if (custom != null)
                query = custom(query);
            return query;
        }

        /// <summary>
        /// Gets Current User ChatRooms With Custom Qurey
        /// </summary>
        /// <param name="custom">Custom Query</param>
        /// <returns></returns>
        /// <exception cref="AuthenticateException">If UserName Was Not Found In Database It Occurs</exception>
        private IQueryable<TblChatRoom> GetChatRooms(Func<IQueryable<TblChatRoom>, IQueryable<TblChatRoom>> custom = null)
        {

            var query = chatRooms;
            query = ChatRoomDefualtQuery(query);
            if (custom != null)
                query = custom(query);
            return query;
        }

        #endregion
    }
}

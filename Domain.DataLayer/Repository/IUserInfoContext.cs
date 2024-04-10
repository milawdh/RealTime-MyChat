using Domain.CustomExceptions;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Domain.DataLayer.UnitOfWorks;
using System.Security.Claims;
using Domain.Entities;

namespace Domain.DataLayer.Repository
{
    public interface IUserInfoContext
    {
        Guid UserId { get; }
        string UserName { get; }
        string? UserChatHubConnectionId { get; }
        TblUsers User { get; }
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
        public IQueryable<TblUsers> GetUser(Func<IQueryable<TblUsers>, IQueryable<TblUsers>> custom = null);
    }

    public class UserInfoContext : IUserInfoContext
    {
        #region Cunstructor

        private readonly HttpContext HttpContext;
        private readonly Core _core;
        private readonly Guid? _userId;

        public UserInfoContext(IHttpContextAccessor httpContextAccessor, Core core)
        {
            HttpContext = httpContextAccessor.HttpContext;
            _core = core;
        }

        public UserInfoContext(Core core, Guid userId)
        {
            _core = core;
            _userId = userId;
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
                i => i.Include(x => x.TblUserChatRoomRel.OrderByDescending(x => x.UserId != UserId)).ThenInclude(x => x.User);
            }
        }

        /// <summary>
        /// TblUser DefualtIncludes
        /// </summary>
        private Func<IQueryable<TblUsers>, IQueryable<TblUsers>> UserDefualtInclude
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
                if (_userId is null)
                {
                    var userName = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!.Value;
                    if (!_core.TblUsers.Any(i => i.UserName == userName))
                        throw new AuthenticateException("UserName Not Found");

                    return _core.TblUsers.Get(i => i.UserName == userName).Select(x => x.Id).FirstOrDefault();
                }
                return _userId.Value;
            }
        }

        /// <summary>
        /// Current User's UserName
        /// </summary>
        public string UserName
        {
            get
            {
                return _core.TblUsers.Get(i => i.Id == UserId).Select(x => x.UserName).FirstOrDefault();
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
        public TblUsers User
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
                return _core.TblUserContacts.Get(x => x.ContactListOwnerId == UserId);
            }
        }

        public IQueryable<TblUserContacts> UserIntegratedContacts
        {
            get
            {
                return _core.TblUserContacts.Get(x => x.ContactUserId == UserId);
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
                    i => i.Include(x => x.TblMessage
                    .Where(x => !x.IsDeleted)
                    .OrderBy(c => c.SendAt))
                    .ThenInclude(x => x.SenderUser).ThenInclude(x => x.ProfileImageUrlNavigation)
                    .Include(x => x.TblMessage.Where(x => !x.IsDeleted).OrderBy(c => c.SendAt))
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
                TblRole tblRole = _core.TblUsers.Get(where: i => i.UserName == UserName,
                    defualtInclude: i => i.Include(x => x.Role).ThenInclude(x => x.TblRolePermissionRel).ThenInclude(x => x.Permission))
                    .FirstOrDefault()!.Role;

                return tblRole.TblRolePermissionRel.Select(i => i.Permission).AsQueryable();

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
        public IQueryable<TblUsers> GetUser(Func<IQueryable<TblUsers>, IQueryable<TblUsers>> custom = null)
        {
            return _core.TblUsers.Get(where: i => i.Id == UserId,
                defualtInclude: UserDefualtInclude,
                includes: custom);
        }

        /// <summary>
        /// Gets Current User ChatRooms With Custom Qurey
        /// </summary>
        /// <param name="custom">Custom Query</param>
        /// <returns></returns>
        /// <exception cref="AuthenticateException">If UserName Was Not Found In Database It Occurs</exception>
        private IQueryable<TblChatRoom> GetChatRooms(Func<IQueryable<TblChatRoom>, IQueryable<TblChatRoom>> custom = null)
        {
            var tblChatRooms = _core.TblChatRoom.Get(
                where: i => i.TblUserChatRoomRel.Select(x => x.UserId).Contains(UserId),
                defualtInclude: ChatRoomDefualtQuery,
                includes: custom
                );

            return tblChatRooms;
        }

        #endregion
    }
}

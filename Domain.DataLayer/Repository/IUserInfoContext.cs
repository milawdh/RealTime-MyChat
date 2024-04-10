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
        private readonly Core core;
        private static string? _userName;
        private readonly DbSet<TblUsers> tblUsers;
        private readonly DbSet<TblUserContacts> tblUserContacts;
        private readonly DbSet<TblChatRoom> chatRooms;
        private readonly DbSet<TblUserChatRoomRel> tblUserChatRooms;

        public UserInfoContext(IHttpContextAccessor httpContextAccessor, AppBaseDbContex context)
        {
            HttpContext = httpContextAccessor.HttpContext;
            tblUsers = context.Set<TblUsers>();
            tblUserContacts = context.Set<TblUserContacts>();
            chatRooms = context.Set<TblChatRoom>();
            tblUserChatRooms = context.Set<TblUserChatRoomRel>();
        }

        public UserInfoContext(AppBaseDbContex context, string userName)
        {
            tblUsers = context.Set<TblUsers>();
            tblUserContacts = context.Set<TblUserContacts>();
            chatRooms = context.Set<TblChatRoom>();
            tblUserChatRooms = context.Set<TblUserChatRoomRel>();
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
                return tblUserContacts.Where(x => x.ContactListOwnerId == UserId);
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
                TblRole tblRole = tblUsers.Where(i => i.UserName == UserName)
                    .Include(x => x.Role).ThenInclude(x => x.TblRolePermissionRel).ThenInclude(x => x.Permission)
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

            var query = chatRooms.Where(i => i.TblUserChatRoomRel.Select(x => x.UserId).Contains(UserId));
            query = ChatRoomDefualtQuery(query);
            if (custom != null)
                query = custom(query);
            return query;
        }

        #endregion
    }
}

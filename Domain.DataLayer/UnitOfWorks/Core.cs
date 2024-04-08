using Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Repositories;
using System.Transactions;
using static System.Net.Mime.ContentType;

namespace Domain.DataLayer.UnitOfWorks
{
    public class Core : IDisposable
    {
        private readonly MyChatContext _context = new MyChatContext();
        private readonly MainRepo<TblChatRoom> ChatRoom;
        private readonly MainRepo<TblImage> Image;
        private readonly MainRepo<TblMedia> Media;
        private readonly MainRepo<TblMessage> Message;
        private readonly MainRepo<TblMyChatIdentifier> MyChatIdentifier;
        private readonly MainRepo<TblPermission> Permission;
        private readonly MainRepo<TblRole> Role;
        private readonly MainRepo<TblRolePermissionRel> RolePermissionRel;
        private readonly MainRepo<TblSettings> Settings;
        private readonly MainRepo<TblUserChatRoomRel> UserChatRoomRel;
        private readonly MainRepo<TblUserContacts> UserContacts;
        private readonly MainRepo<TblUserImageRel> UserImageRel;
        private readonly MainRepo<TblUsers> Users;



        public MainRepo<TblImage> TblImage => Image ?? new(_context);
        public MainRepo<TblChatRoom> TblChatRoom => ChatRoom ?? new(_context);
        public MainRepo<TblMedia> TblMedia => Media ?? new(_context);
        public MainRepo<TblMessage> TblMessage => Message ?? new(_context);
        public MainRepo<TblMyChatIdentifier> TblMyChatIdentifier => MyChatIdentifier ?? new(_context);
        public MainRepo<TblPermission> TblPermission => Permission ?? new(_context);
        public MainRepo<TblRole> TblRole => Role ?? new(_context);
        public MainRepo<TblRolePermissionRel> TblRolePermissionRel => RolePermissionRel ?? new(_context);
        public MainRepo<TblSettings> TblSettings => Settings ?? new(_context);
        public MainRepo<TblUserChatRoomRel> TblUserChatRoomRel => UserChatRoomRel ?? new(_context);
        public MainRepo<TblUserContacts> TblUserContacts => UserContacts ?? new(_context);
        public MainRepo<TblUserImageRel> TblUserImageRel => UserImageRel ?? new(_context);
        public MainRepo<TblUsers> TblUsers => Users ?? new(_context);



        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose() => _context.Dispose();
        public IDbContextTransaction BeginTransaction() => _context.BeginTransaction();
        public void CommitTransaction() => _context.CommitTransaction();
        public void RollBackTransaction() => _context.RollbackTransaction();

        public static void SavePoint(IDbContextTransaction transaction, string point) => transaction.CreateSavepoint(point);
        public static void RollBackToSavePoint(IDbContextTransaction transaction, string point) => transaction.RollbackToSavepoint(point);

    }
}

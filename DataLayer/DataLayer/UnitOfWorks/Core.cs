using Domain.API;
using Domain.Audited.Models;
using Domain.DataLayer.Contexts.Base;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Repositories;

namespace Domain.DataLayer.UnitOfWorks
{
    public class Core
    {
        private readonly AppBaseDbContex _context;
        public Core(AppBaseDbContex context)
        {
            _context = context;
        }
        private readonly MainRepo<TblChatRoom> ChatRoom;
        private readonly MainRepo<TblImage> Image;
        private readonly MainRepo<TblMedia> Media;
        private readonly MainRepo<TblMessage> Message;
        private readonly MainRepo<TblMyChatIdentifier> MyChatIdentifier;
        private readonly MainRepo<TblRole> Role;
        private readonly MainRepo<TblRolePermissionRel> RolePermissionRel;
        private readonly MainRepo<TblSetting> Settings;
        private readonly MainRepo<TblUserChatRoomRel> UserChatRoomRel;
        private readonly MainRepo<TblUserContacts> UserContacts;
        private readonly MainRepo<TblUserImageRel> UserImageRel;
        private readonly MainRepo<TblUser> Users;
        private readonly MainRepo<TblFileServer> FileServer;



        public MainRepo<TblImage> TblImage => Image ?? new(_context);
        public MainRepo<TblChatRoom> TblChatRoom => ChatRoom ?? new(_context);
        public MainRepo<TblMedia> TblMedia => Media ?? new(_context);
        public MainRepo<TblMessage> TblMessage => Message ?? new(_context);
        public MainRepo<TblMyChatIdentifier> TblMyChatIdentifier => MyChatIdentifier ?? new(_context);
        public MainRepo<TblRole> TblRole => Role ?? new(_context);
        public MainRepo<TblRolePermissionRel> TblRolePermissionRel => RolePermissionRel ?? new(_context);
        public MainRepo<TblSetting> TblSettings => Settings ?? new(_context);
        public MainRepo<TblUserChatRoomRel> TblUserChatRoomRel => UserChatRoomRel ?? new(_context);
        public MainRepo<TblUserContacts> TblUserContacts => UserContacts ?? new(_context);
        public MainRepo<TblUserImageRel> TblUserImageRel => UserImageRel ?? new(_context);
        public MainRepo<TblUser> TblUsers => Users ?? new(_context);
        public MainRepo<TblFileServer> TblFileServer => FileServer ?? new(_context);

        public void Dispose()
        {
            _context.Dispose();
        }


        #region Transaction
        /// <summary>
        /// Opens a Transaction on Current DataBase
        /// </summary>
        /// <returns>Returns The Transaction Opened</returns>
        public IDbContextTransaction BeginTransaction() => _context.BeginTransaction();
        /// <summary>
        /// Commits Opened Transaction to DataBase
        /// </summary>
        public void CommitTransaction() => _context.CommitTransaction();
        /// <summary>
        /// Rolls Back All Transaction Proccess
        /// </summary>
        public void RollBackTransaction() => _context.RollbackTransaction();

        #endregion

        #region Save Points

        /// <summary>
        /// Saves A point to transaction proccess to roll back on it in exceptions or special situations
        /// </summary>
        /// <param name="transaction">Current Opened Transaction</param>
        /// <param name="point">Save Point Name to assaign</param>
        public static void SavePoint(IDbContextTransaction transaction, string point) => transaction.CreateSavepoint(point);
        /// <summary>
        /// Gets Back to specified point on the transaction
        /// </summary>
        /// <param name="transaction">Current Opened Transaction</param>
        /// <param name="point">Save Point Name That you assaigned on transaction</param>
        public static void RollBackToSavePoint(IDbContextTransaction transaction, string point) => transaction.RollbackToSavepoint(point);

        #endregion

        #region Helpers

        /// <summary>
        /// Executes T-Sql NonQuery(With No Result to return) command to current Database with ADO.Net
        /// </summary>
        /// <param name="command">T-Sql command in string Line</param>
        /// <returns>Returns Exception if it occures in execyting proccess</returns>
        public ServiceResult<Exception> ExecuteNonQueryCommand(string command)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(GetCurrentConnectionString()))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }
                return new ServiceResult<Exception>();
            }
            catch (Exception ex)
            {
                return new ServiceResult<Exception>(ex, "An Error occured: " + ex.Message) { Failure = true, Success = false };
            }
        }

        /// <summary>
        /// Returns Entity DbSet! Use it in specific Entities that are not inherited <see cref="Audited.Models.Entity{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <returns>Entity's DbSet!</returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class => _context.Set<TEntity>();

        /// <summary>
        /// Marks Entity As Updated and Implement Audited Modification In Entity (Without Validation)! Use it on special Situations!
        /// </summary>
        /// <typeparam name="TEntity">Entity Type That Inherits Base Entity Class <see cref="Audited.Models.Entity{TEntity}"/></typeparam>
        /// <param name="entity">Entity to Update</param>
        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : Entity<TEntity>
        {
            _context.Update(entity);
        }

        /// <summary>
        /// Saves Changes on the current Transaction
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns Current DbContext Connection string
        /// </summary>
        /// <returns></returns>
        public string GetCurrentConnectionString() =>
            _context.GetCurrentConnectionString();
        #endregion
    }
}

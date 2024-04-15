using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Audited.Api;
using Domain.API;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Domain.DataLayer.UnitOfWorks;
using Domain.DataLayer.Repository;
using Domain.Entities;

namespace Domain.Audited.Models
{
    public interface IBaseAuditedEntity
    {
        public bool IsDeleted { get; set; }
    }

    public class Entity<TEntity> : AuditedValidation<TEntity>, IBaseAuditedEntity where TEntity : class
    {
        public bool IsDeleted { get; set; } = false;


        /// <summary>
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity Thath Will be Validated</param>
        /// <returns>ValidationResult</returns>
        public override ServiceResult ValidateAdd(TEntity entity, Core core)
        {
            return base.ValidateAdd(entity, core);
        }

        /// <summary>
        /// if you don't override it No Filteration Will be invoked!
        /// </summary>
        /// <param name="entities">DbSet's In IQueryable That Will Be Filtered</param>
        /// <returns>A Validated and Filtered Entities Query</returns>
        public override IQueryable<TEntity> ValidateGetPermission(Core core, IQueryable<TEntity> entities, IUserInfoContext userInfoContext)
        {
            return base.ValidateGetPermission(core, entities, userInfoContext);
        }

        /// <summary>
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity Thath Will be Validated</param>
        /// <returns>ValidationResult</returns>
        public override ServiceResult ValidateRemove(TEntity entity, Core core)
        {
            return base.ValidateRemove(entity, core);
        }

        /// <summary>
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity Thath Will be Validated</param>
        /// <returns>ValidationResult</returns>
        public override ServiceResult ValidateUpdate(TEntity entity, Core core)
        {
            return base.ValidateUpdate(entity, core);
        }
    }

    /// <summary>
    /// Base Entity For All Entities that Has Primary Key and Validators for Override
    /// if you don't override them validate will be success at all
    /// </summary>
    /// <typeparam name="TKey">PrimaryKey of Entity</typeparam>
    [PrimaryKey("Id")]
    public abstract class BaseAuditedEntity<TEntity, TKey> : Entity<TEntity> where TEntity : class
    {
        [Key]
        [Column("ID")]
        public TKey Id { get; set; }
    }
}

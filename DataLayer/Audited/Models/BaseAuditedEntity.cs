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

namespace Domain.Audited.Models
{
    public interface IBaseAuditedEntity
    {
        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// Base Entity For All Entities that Has Primary Key and Validators for Override
    /// if you don't override them validate will be success at all
    /// </summary>
    /// <typeparam name="TKey">PrimaryKey of Entity</typeparam>
    [PrimaryKey("Id")]
    public abstract class BaseAuditedEntity<TKey> : IAuditedValidation, IBaseAuditedEntity
    {
        [Key]
        [Column("ID")]
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity you will validate</param>
        /// <returns>ValidationResul</returns>
        public virtual ServiceResult ValidateAdd(object entity)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity you will validate</param>
        /// <returns>ValidationResul</returns>
        public virtual ServiceResult ValidateUpdate(object entity)
        {
            return new ServiceResult();
        }
    }
}

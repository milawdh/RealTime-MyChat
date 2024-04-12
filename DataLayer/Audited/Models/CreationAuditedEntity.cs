using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Audited.Models
{
    public interface ICreationAuditedEntity
    {
        public TblUser CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// Inherits Base Entity And Has Add Shadow Props
    /// </summary>
    /// <typeparam name="TUser">User's Entity</typeparam>
    /// <typeparam name="TUserKey">User's Primary Key</typeparam>
    /// <typeparam name="TKey">Entity's Primary Key</typeparam>
    public abstract class CreationAuditedEntity<TEntity,TKey> : BaseAuditedEntity<TEntity,TKey> , ICreationAuditedEntity where TEntity : class
    {
        [ForeignKey("CreatedById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual TblUser CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } 
    }
}

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
    public interface IModificationAuditedEntity 
    {
        [ForeignKey("ModifiedById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public TblUser? ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }

    /// <summary>
    /// Has Base & Add And Update Shadow Properties
    /// </summary>
    /// <typeparam name="TKey">Entity's Primary Key</typeparam>
    public abstract class ModificationAuditedEntity<TEntity,TKey> : CreationAuditedEntity<TEntity, TKey>, IModificationAuditedEntity where TEntity : class
    {
        [ForeignKey("ModifiedById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual TblUser? ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }
}

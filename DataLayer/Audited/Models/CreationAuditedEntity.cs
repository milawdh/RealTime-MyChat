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

    }

    /// <summary>
    /// Inherits Base Entity And Has Add Shadow Props
    /// </summary>
    /// <typeparam name="TUser">User's Entity</typeparam>
    /// <typeparam name="TUserKey">User's Primary Key</typeparam>
    /// <typeparam name="TKey">Entity's Primary Key</typeparam>
    public abstract class CreationAuditedEntity<TUser, TKey> : BaseAuditedEntity<TKey> , ICreationAuditedEntity
    {
        [ForeignKey("CreatedById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public TUser CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}

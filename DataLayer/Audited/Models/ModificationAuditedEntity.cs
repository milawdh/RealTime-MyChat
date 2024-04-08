using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Audited.Models
{
    /// <summary>
    /// Has Base & Add And Update Shadow Properties
    /// </summary>
    /// <typeparam name="TUser">User's Entity</typeparam>
    /// <typeparam name="TUserKey">User's Primary Key</typeparam>
    /// <typeparam name="TKey">Entity's Primary Key</typeparam>
    public abstract class ModificationAuditedEntity<TUser, TKey> : CreationAuditedEntity<TUser, TKey>
    {
        [ForeignKey("ModifiedById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public TUser? ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Audited.Models
{
    /// <summary>
    /// Has Base & Add & Update And Delete Shadow Properties
    /// </summary>
    /// <typeparam name="TUser">User's Entity</typeparam>
    /// <typeparam name="TUserKey">User's Primary Key</typeparam>
    /// <typeparam name="TKey">Entity's Primary Key</typeparam>
    public abstract class FullAuditedEntity<TUser, TKey> : ModificationAuditedEntity<TUser, TKey>
    {
        [ForeignKey("DeleteById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public TUser? DeleteBy { get; set; }
        public Guid? DeleteById { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}

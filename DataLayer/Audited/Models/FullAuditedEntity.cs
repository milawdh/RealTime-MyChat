﻿using Domain.Entities;
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
    public interface IFullAuditedEntity 
    {
        [ForeignKey("DeleteById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public TblUsers? DeleteBy { get; set; }
        public Guid? DeleteById { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
    /// <summary>
    /// Has Base & Add & Update And Delete Shadow Properties
    /// </summary>
    /// <typeparam name="TUser">User's Entity</typeparam>
    /// <typeparam name="TUserKey">User's Primary Key</typeparam>
    /// <typeparam name="TKey">Entity's Primary Key</typeparam>
    public abstract class FullAuditedEntity<TKey> : ModificationAuditedEntity<TKey> , IFullAuditedEntity
    {
        [ForeignKey("DeleteById")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public TblUsers? DeleteBy { get; set; }
        public Guid? DeleteById { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}

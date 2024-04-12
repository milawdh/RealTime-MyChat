using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class TblUserContacts : Entity<TblUserContacts>, IModificationAuditedEntity, ICreationAuditedEntity
{
    public Guid CreatedById { get; set; }

    public Guid ContactUserId { get; set; }

    [StringLength(32)]
    public string ContactName { get; set; } = null!;

    [InverseProperty(nameof(TblUser.TblUserContactsContactListCreateds))]
    public virtual TblUser CreatedBy { get; set; } = null!;
    [InverseProperty(nameof(TblUser.TblUserContactsContactUsers))]
    public virtual TblUser ContactUser { get; set; } = null!;

    public TblUser? ModifiedBy { get; set; }
    public Guid? ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}

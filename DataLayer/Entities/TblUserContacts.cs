using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class TblUserContacts
{
    public Guid ContactListOwnerId { get; set; }

    public Guid ContactUserId { get; set; }

    [StringLength(32)]
    public string ContactName { get; set; } = null!;

    public virtual TblUsers ContactListOwner { get; set; } = null!;

    public virtual TblUsers ContactUser { get; set; } = null!;
}

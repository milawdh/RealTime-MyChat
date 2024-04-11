using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblSetting : FullAuditedEntity<int>
{
    public bool ShowOnline { get; set; }

    public bool ShowLastSeen { get; set; }

    public bool ShowProfilePhoto { get; set; }

    /// <summary>
    /// 0 NoBody
    /// 1 MyContacts
    /// 2 EveryBody
    /// 
    /// </summary>
    public short ShowPhoneNumber { get; set; }

    [InverseProperty(nameof(TblUser.Setting))]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class TblSettings
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

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

    [InverseProperty("Settings")]
    public virtual ICollection<TblUsers> TblUsers { get; set; } = new List<TblUsers>();
}

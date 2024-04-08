using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblRole
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = null!;

    public bool IsCostume { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<TblRolePermissionRel> TblRolePermissionRel { get; set; } = new List<TblRolePermissionRel>();

    [InverseProperty("Role")]
    public virtual ICollection<TblUsers> TblUsers { get; set; } = new List<TblUsers>();
}

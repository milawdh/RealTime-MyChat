using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblRole : FullAuditedEntity<Guid>
{
    [StringLength(32)]
    public string Name { get; set; } = null!;

    public bool IsCostume { get; set; }

    [InverseProperty(nameof(TblRolePermissionRel.Role))]
    public virtual ICollection<TblRolePermissionRel> TblRolePermissionRels { get; set; } = new List<TblRolePermissionRel>();

    [InverseProperty(nameof(TblUser.Role))]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}

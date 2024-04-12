using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblRolePermissionRel : FullAuditedEntity<TblRolePermissionRel,Guid>
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    [ForeignKey(nameof(PermissionId))]
    [InverseProperty(nameof(TblPermission.TblRolePermissionRels))]
    public virtual TblPermission Permission { get; set; } = null!;

    [ForeignKey(nameof(RoleId))]
    [InverseProperty(nameof(TblRole.TblRolePermissionRels))]
    public virtual TblRole Role { get; set; } = null!;
}

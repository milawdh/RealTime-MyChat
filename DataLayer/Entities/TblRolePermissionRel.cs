using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblRolePermissionRel
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("TblRolePermissionRel")]
    public virtual TblPermission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("TblRolePermissionRel")]
    public virtual TblRole Role { get; set; } = null!;
}

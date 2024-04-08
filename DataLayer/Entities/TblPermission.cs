using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblPermission
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(64)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Permission")]
    public virtual ICollection<TblRolePermissionRel> TblRolePermissionRel { get; set; } = new List<TblRolePermissionRel>();
}

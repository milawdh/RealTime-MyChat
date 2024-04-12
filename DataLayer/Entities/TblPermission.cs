using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblPermission : FullAuditedEntity<TblPermission,Guid>
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(64)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty(nameof(TblRolePermissionRel.Permission))]
    public virtual ICollection<TblRolePermissionRel> TblRolePermissionRels { get; set; } = new List<TblRolePermissionRel>();
}

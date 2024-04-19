using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
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


    public Guid? ParentId { get; set; }
    [ForeignKey(name: nameof(ParentId))]
    public virtual TblPermission Parent { get; set; }

    [InverseProperty(nameof(Parent))]
    public virtual ICollection<TblPermission> Children { get; set; } = new List<TblPermission>();

    [InverseProperty(nameof(TblRolePermissionRel.Permission))]
    public virtual ICollection<TblRolePermissionRel> TblRolePermissionRels { get; set; } = new List<TblRolePermissionRel>();
    public virtual ICollection<TblUserChatRoomMapPermission> TblUserChatRoomMapPermissions { get; set; } = new List<TblUserChatRoomMapPermission>();
}

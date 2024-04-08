using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index("Tell", Name = "IX_TblUsers_Tell", IsUnique = true)]
public partial class TblUsers
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [StringLength(16)]
    [Unicode(false)]
    public string Tell { get; set; } = null!;

    [StringLength(32)]
    public string Name { get; set; } = null!;

    [StringLength(32)]
    public string? LastName { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(64)]
    public string? Bio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateSigned { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastOnline { get; set; }

    public Guid ProfileImageUrl { get; set; }

    public bool IsDeleted { get; set; }

    public string? ConnectionId { get; set; }

    public Guid RoleId { get; set; }

    public bool IsOnline { get; set; }

    public int SettingsId { get; set; }

    [ForeignKey("ProfileImageUrl")]
    [InverseProperty("TblUsers")]
    public virtual TblImage ProfileImageUrlNavigation { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("TblUsers")]
    public virtual TblRole Role { get; set; } = null!;

    [ForeignKey("SettingsId")]
    [InverseProperty("TblUsers")]
    public virtual TblSettings Settings { get; set; } = null!;

    [InverseProperty("Owner")]
    public virtual ICollection<TblChatRoom> TblChatRoom { get; set; } = new List<TblChatRoom>();

    [InverseProperty("SenderUser")]
    public virtual ICollection<TblMessage> TblMessage { get; set; } = new List<TblMessage>();

    [InverseProperty("User")]
    public virtual ICollection<TblUserChatRoomRel> TblUserChatRoomRel { get; set; } = new List<TblUserChatRoomRel>();

    [InverseProperty("ContactListOwner")]
    public virtual ICollection<TblUserContacts> TblUserContactsContactListOwner { get; set; } = new List<TblUserContacts>();

    [InverseProperty("ContactUser")]
    public virtual ICollection<TblUserContacts> TblUserContactsContactUser { get; set; } = new List<TblUserContacts>();

    [InverseProperty("User")]
    public virtual ICollection<TblUserImageRel> TblUserImageRel { get; set; } = new List<TblUserImageRel>();

    [ForeignKey("UserName")]
    [InverseProperty("TblUsers")]
    public virtual TblMyChatIdentifier UserNameNavigation { get; set; } = null!;
}

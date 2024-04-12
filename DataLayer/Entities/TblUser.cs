using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index("Tell", Name = "IX_TblUsers_Tell", IsUnique = true)]
public partial class TblUser : Entity<TblUser>
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

    public string? ConnectionId { get; set; }

    public Guid RoleId { get; set; }

    public bool IsOnline { get; set; }

    public int SettingId { get; set; }

    [ForeignKey(nameof(ProfileImageUrl))]
    [InverseProperty(nameof(TblImage.TblUsers))]
    public virtual TblImage ProfileImageUrlNavigation { get; set; } = null!;

    [ForeignKey(nameof(RoleId))]
    [InverseProperty(nameof(TblRole.TblUsers))]
    public virtual TblRole Role { get; set; } = null!;

    [ForeignKey(nameof(SettingId))]
    [InverseProperty(nameof(TblSetting.TblUsers))]
    public virtual TblSetting Setting { get; set; } = null!;

    [InverseProperty(nameof(TblChatRoom.CreatedBy))]
    public virtual ICollection<TblChatRoom> TblChatRooms { get; set; } = new List<TblChatRoom>();

    [InverseProperty(nameof(TblMessage.CreatedBy))]
    public virtual ICollection<TblMessage> TblMessages { get; set; } = new List<TblMessage>();

    [InverseProperty(nameof(TblUserChatRoomRel.User))]
    public virtual ICollection<TblUserChatRoomRel> TblUserChatRoomRels { get; set; } = new List<TblUserChatRoomRel>();

    [InverseProperty(nameof(TblUserContacts.CreatedBy))]
    public virtual ICollection<TblUserContacts> TblUserContactsContactListCreateds { get; set; } = new List<TblUserContacts>();

    [InverseProperty(nameof(TblUserContacts.ContactUser))]
    public virtual ICollection<TblUserContacts> TblUserContactsContactUsers { get; set; } = new List<TblUserContacts>();

    [InverseProperty(nameof(TblUserImageRel.User))]
    public virtual ICollection<TblUserImageRel> TblUserImageRels { get; set; } = new List<TblUserImageRel>();

    [ForeignKey(nameof(UserName))]
    [InverseProperty(nameof(TblMyChatIdentifier.TblUsers))]
    public virtual TblMyChatIdentifier UserNameNavigation { get; set; } = null!;
}

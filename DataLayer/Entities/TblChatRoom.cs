using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblChatRoom
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public Guid? ParentId { get; set; }

    [Required]
    public ChatRoomType Type { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string? MyChatId { get; set; }

    [StringLength(64)]
    public string? ChatRoomTitle { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    public Guid? ProfileImageId { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<TblChatRoom> InverseParent { get; set; } = new List<TblChatRoom>();

    [ForeignKey("MyChatId")]
    [InverseProperty("TblChatRoom")]
    public virtual TblMyChatIdentifier? MyChat { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TblChatRoom")]
    public virtual TblUsers Owner { get; set; } = null!;

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual TblChatRoom? Parent { get; set; }

    [ForeignKey("ProfileImageId")]
    [InverseProperty("TblChatRoom")]
    public virtual TblImage? ProfileImage { get; set; }

    [InverseProperty("RecieverChatRoom")]
    public virtual ICollection<TblMessage> TblMessage { get; set; } = new List<TblMessage>();

    [InverseProperty("ChatRoom")]
    public virtual ICollection<TblUserChatRoomRel> TblUserChatRoomRel { get; set; } = new List<TblUserChatRoomRel>();

}

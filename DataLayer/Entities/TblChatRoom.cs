using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Api;
using Domain.Audited.Models;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblChatRoom : FullAuditedEntity<TblChatRoom,Guid>
{
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

    [InverseProperty(nameof(Parent))]
    public virtual ICollection<TblChatRoom> InverseParent { get; set; } = new List<TblChatRoom>();

    [ForeignKey(nameof(MyChatId))]
    [InverseProperty(nameof(TblMyChatIdentifier.TblChatRooms))]
    public virtual TblMyChatIdentifier? MyChat { get; set; }

    [InverseProperty(nameof(TblUser.TblChatRooms))]
    public virtual TblUser CreatedBy { get; set; } = null!;

    [ForeignKey(nameof(ParentId))]
    [InverseProperty(nameof(InverseParent))]
    public virtual TblChatRoom? Parent { get; set; }

    [ForeignKey(nameof(ProfileImageId))]
    [InverseProperty(nameof(TblImage.TblChatRooms))]
    public virtual TblImage? ProfileImage { get; set; }

    [InverseProperty(nameof(TblMessage.RecieverChatRoom))]
    public virtual ICollection<TblMessage> TblMessages { get; set; } = new List<TblMessage>();

    [InverseProperty(nameof(TblUserChatRoomRel.ChatRoom))]
    public virtual ICollection<TblUserChatRoomRel> TblUserChatRoomRels { get; set; } = new List<TblUserChatRoomRel>();



    #region Validations

    public override IQueryable<TblChatRoom> ValidateGetPermission(Core core, IQueryable<TblChatRoom> entities, IUserInfoContext userInfoContext)
    {
       return entities.Where(i => i.TblUserChatRoomRels.Select(x => x.UserId).Contains(userInfoContext.UserId));
    }

    #endregion
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using Domain.JsonFieldModels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblMessage : FullAuditedEntity<TblMessage,Guid>
{
    public Guid? ReplyId { get; set; }

    [StringLength(1024)]
    public string Body { get; set; } = null!;

    public Guid RecieverChatRoomId { get; set; }

    public bool IsFromSystem { get; set; }

    public ICollection<TblUserChatRoomRel> ReadedBys { get; set; }

    [InverseProperty(nameof(Reply))]
    public virtual ICollection<TblMessage> InverseReplys { get; set; } = new List<TblMessage>();

    [ForeignKey(nameof(RecieverChatRoomId))]
    [InverseProperty(nameof(TblChatRoom.TblMessages))]
    public virtual TblChatRoom RecieverChatRoom { get; set; } = null!;

    [ForeignKey(nameof(ReplyId))]
    [InverseProperty(nameof(InverseReplys))]
    public virtual TblMessage? Reply { get; set; }

    [InverseProperty(nameof(TblUser.TblMessages))]
    public virtual TblUser CreatedBy { get; set; } = null!;

    [InverseProperty(nameof(TblMedia.Message))]
    public virtual ICollection<TblMedia> TblMedias { get; set; } = new List<TblMedia>();

    #region Validations

    public override IQueryable<TblMessage> ValidateGetPermission(Core core ,IQueryable<TblMessage> entities, IUserInfoContext userInfoContext)
    {
        return base.ValidateGetPermission(core,entities.Where(x => userInfoContext.ChatRooms.Any(v => v.Id == x.RecieverChatRoomId)), userInfoContext);
    }

    #endregion
}

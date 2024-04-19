using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblUserChatRoomRel : FullAuditedEntity<TblUserChatRoomRel,Guid>
{
    public Guid ChatRoomId { get; set; }

    public Guid UserId { get; set; }

    public TblMessage LastSeenMessage { get; set; } = null;
    public Guid? LastSeenMessageId { get; set; }

    [ForeignKey(nameof(ChatRoomId))]
    [InverseProperty(nameof(TblChatRoom.TblUserChatRoomRels))]
    public virtual TblChatRoom ChatRoom { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(TblUser.TblUserChatRoomRels))]
    public virtual TblUser User { get; set; } = null!;
    public virtual ICollection<TblUserChatRoomMapPermission> TblUserChatRoomMapPermissions { get; set; } = new List<TblUserChatRoomMapPermission>();
}

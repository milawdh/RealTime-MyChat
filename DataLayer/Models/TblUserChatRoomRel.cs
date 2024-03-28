using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class TblUserChatRoomRel
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public Guid ChatRoomId { get; set; }

    public Guid UserId { get; set; }


    public TblMessage LastSeenMessage { get; set; } = null;
    public Guid? LastSeenMessageId { get; set; }

    [ForeignKey("ChatRoomId")]
    [InverseProperty("TblUserChatRoomRel")]
    public virtual TblChatRoom ChatRoom { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TblUserChatRoomRel")]
    public virtual TblUsers User { get; set; } = null!;
}

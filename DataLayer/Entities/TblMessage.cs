using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Domain.JsonFieldModels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblMessage : FullAuditedEntity<Guid>
{
    public Guid? ReplyId { get; set; }

    [StringLength(1024)]
    public string Body { get; set; } = null!;

    public Guid SenderUserId { get; set; }

    public Guid RecieverChatRoomId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SendAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedAt { get; set; }

    public bool IsEdited { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsFromSystem { get; set; }

    public ICollection<TblUserChatRoomRel> ReadedBys { get; set; }

    [InverseProperty("Reply")]
    public virtual ICollection<TblMessage> InverseReply { get; set; } = new List<TblMessage>();

    [ForeignKey("RecieverChatRoomId")]
    [InverseProperty("TblMessage")]
    public virtual TblChatRoom RecieverChatRoom { get; set; } = null!;

    [ForeignKey("ReplyId")]
    [InverseProperty("InverseReply")]
    public virtual TblMessage? Reply { get; set; }

    [ForeignKey("SenderUserId")]
    [InverseProperty("TblMessage")]
    public virtual TblUsers SenderUser { get; set; } = null!;

    [InverseProperty("Message")]
    public virtual ICollection<TblMedia> TblMedia { get; set; } = new List<TblMedia>();
}

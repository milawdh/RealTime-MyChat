using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblMedia : FullAuditedEntity<TblMedia, Guid>
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public MediaType Type { get; set; }

    public Guid MessageId { get; set; }

    [StringLength(128)]
    public string Url { get; set; } = null!;

    [ForeignKey(nameof(MessageId))]
    [InverseProperty(nameof(TblMessage.TblMedias))]
    public virtual TblMessage Message { get; set; } = null!;
}

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
    public MediaType MediaType { get; set; }

    public string FileName { get; set; }
    public string FileMimType { get; set; }

    [ForeignKey(nameof(FileServerId))]
    [InverseProperty(nameof(TblFileServer.TblMedias))]
    public TblFileServer FileServer { get; set; }
    public Guid FileServerId { get; set; }


    public Guid MessageId { get; set; }

    [ForeignKey(nameof(MessageId))]
    [InverseProperty(nameof(TblMessage.TblMedias))]
    public virtual TblMessage Message { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblMedia
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public MediaType Type { get; set; }

    public Guid MessageId { get; set; }

    [StringLength(128)]
    public string Url { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [ForeignKey("MessageId")]
    [InverseProperty("TblMedia")]
    public virtual TblMessage Message { get; set; } = null!;
}

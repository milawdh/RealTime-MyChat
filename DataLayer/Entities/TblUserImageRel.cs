using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblUserImageRel
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    public Guid ImageUrl { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("ImageUrl")]
    [InverseProperty("TblUserImageRel")]
    public virtual TblImage ImageUrlNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TblUserImageRel")]
    public virtual TblUsers User { get; set; } = null!;
}

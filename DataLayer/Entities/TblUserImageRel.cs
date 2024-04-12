using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblUserImageRel : FullAuditedEntity<TblUserImageRel,Guid>
{
    public Guid ImageUrl { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey(nameof(ImageUrl))]
    [InverseProperty(nameof(TblImage.TblUserImageRels))]
    public virtual TblImage ImageUrlNavigation { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(TblUser.TblUserImageRels))]
    public virtual TblUser User { get; set; } = null!;
}

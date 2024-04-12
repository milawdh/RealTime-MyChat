using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Audited.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblImage : FullAuditedEntity<TblImage, Guid>
{
    [StringLength(128)]
    public string Url { get; set; } = null!;

    [InverseProperty(nameof(TblChatRoom.ProfileImage))]
    public virtual ICollection<TblChatRoom> TblChatRooms { get; set; } = new List<TblChatRoom>();

    [InverseProperty(nameof(TblUserImageRel.ImageUrlNavigation))]
    public virtual ICollection<TblUserImageRel> TblUserImageRels { get; set; } = new List<TblUserImageRel>();

    [InverseProperty(nameof(TblUser.ProfileImageUrlNavigation))]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}

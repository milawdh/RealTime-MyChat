using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblImage
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [StringLength(128)]
    public string Url { get; set; } = null!;

    [InverseProperty("ProfileImage")]
    public virtual ICollection<TblChatRoom> TblChatRoom { get; set; } = new List<TblChatRoom>();

    [InverseProperty("ImageUrlNavigation")]
    public virtual ICollection<TblUserImageRel> TblUserImageRel { get; set; } = new List<TblUserImageRel>();

    [InverseProperty("ProfileImageUrlNavigation")]
    public virtual ICollection<TblUsers> TblUsers { get; set; } = new List<TblUsers>();
}

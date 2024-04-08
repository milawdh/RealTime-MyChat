using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class TblMyChatIdentifier
{
    [Key]
    [StringLength(32)]
    [Unicode(false)]
    public string Identifier { get; set; } = null!;

    [InverseProperty("MyChat")]
    public virtual ICollection<TblChatRoom> TblChatRoom { get; set; } = new List<TblChatRoom>();

    [InverseProperty("UserNameNavigation")]
    public virtual ICollection<TblUsers> TblUsers { get; set; } = new List<TblUsers>();
}

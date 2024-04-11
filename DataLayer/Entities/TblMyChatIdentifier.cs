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

    [InverseProperty(nameof(TblChatRoom.MyChat))]
    public virtual ICollection<TblChatRoom> TblChatRooms { get; set; } = new List<TblChatRoom>();

    [InverseProperty(nameof(TblUser.UserNameNavigation))]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}

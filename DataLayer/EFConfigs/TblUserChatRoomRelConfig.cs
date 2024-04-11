using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configs
{
    public class TblUserChatRoomRelConfig : IEntityTypeConfiguration<TblUserChatRoomRel>
    {
        public void Configure(EntityTypeBuilder<TblUserChatRoomRel> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(d => d.ChatRoom).WithMany(p => p.TblUserChatRoomRels)
                .HasForeignKey(e => e.ChatRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUserChatRoomRel_TblChatRoom");

            builder.HasOne(x => x.LastSeenMessage)
                .WithMany(p => p.ReadedBys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(e => e.LastSeenMessageId);

            builder.HasOne(d => d.User).WithMany(p => p.TblUserChatRoomRels)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUserChatRoomRel_TblUsers");
        }
    }
}

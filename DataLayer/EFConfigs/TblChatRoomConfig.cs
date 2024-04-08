using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Configs
{
    public class TblChatRoomConfig : IEntityTypeConfiguration<TblChatRoom>
    {
        public void Configure(EntityTypeBuilder<TblChatRoom> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            builder.Property(e => e.OwnerId).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(d => d.MyChat).WithMany(p => p.TblChatRoom)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblChatRoom_TblMyChatIdentifier");

            builder.HasOne(d => d.Owner).WithMany(p => p.TblChatRoom)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblChatRoom_TblUsers");


            builder.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_TblChatRoom_TblChatRoom");

            builder.Property(e => e.Type).HasConversion<short>();

            builder.HasOne(d => d.ProfileImage).WithMany(p => p.TblChatRoom)
                .HasForeignKey(e => e.ProfileImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TblChatRoom_TblImage");
        }
    }
}

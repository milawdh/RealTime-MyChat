using Domain.JsonFieldModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Configs
{
    public class TblMessageConfig : IEntityTypeConfiguration<TblMessage>
    {
        public void Configure(EntityTypeBuilder<TblMessage> builder)
        {

            #region Converstations

            builder.HasQueryFilter(q => !q.IsDeleted);

            #endregion

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(d => d.RecieverChatRoom).WithMany(p => p.TblMessages)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(e => e.RecieverChatRoomId)
                    .HasConstraintName("FK_TblMessage_TblChatRoom");

            builder.HasOne(d => d.Reply).WithMany(p => p.InverseReplys).HasConstraintName("FK_TblMessage_TblMessage");

            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.TblMessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(e => e.CreatedById)
                .HasConstraintName("FK_TblMessage_TblUsers");
        }
    }
}

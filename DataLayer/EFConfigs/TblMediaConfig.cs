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
    public class TblMediaConfig : IEntityTypeConfiguration<TblMedia>
    {
        public void Configure(EntityTypeBuilder<TblMedia> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.Property(e => e.MediaType).HasConversion<short>();

            builder.HasOne(x => x.FileServer)
                .WithMany(p => p.TblMedias)
                .HasForeignKey(x => x.FileServerId)
                .HasConstraintName("FK_TBlMedia_TblFileServer")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.FileServerId).HasDefaultValue(new Guid("3b2eceb8-fbf8-ee11-b6e5-44af284397a1"));

            builder.HasOne(d => d.Message)
                .WithMany(p => p.TblMedias)
                .HasForeignKey(e => e.MessageId)
                .HasConstraintName("FK_TblMedia_TblMessage");
        }
    }
}

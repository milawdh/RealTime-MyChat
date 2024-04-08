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
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            builder.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Type).HasConversion<short>();

            builder.HasOne(d => d.Message)
                .WithMany(p => p.TblMedia)
                .HasForeignKey(e => e.MessageId)
                .HasConstraintName("FK_TblMedia_TblMessage");
        }
    }
}

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
    public class TblUserImageRelConfig : IEntityTypeConfiguration<TblUserImageRel>
    {
        public void Configure(EntityTypeBuilder<TblUserImageRel> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(d => d.ImageUrlNavigation)
                .WithMany(p => p.TblUserImageRel)
                .HasForeignKey(e => e.ImageUrl)
                .HasConstraintName("FK_TblUserImageRel_TblImage");

            builder.HasOne(d => d.User)
                .WithMany(p => p.TblUserImageRel)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_TblUserImageRel_TblUsers");
        }
    }
}

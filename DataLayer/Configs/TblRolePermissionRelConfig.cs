using Domain.Models;
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
    public class TblRolePermissionRelConfig : IEntityTypeConfiguration<TblRolePermissionRel>
    {
        public void Configure(EntityTypeBuilder<TblRolePermissionRel> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(d => d.Permission)
                .WithMany(p => p.TblRolePermissionRel)
                .HasForeignKey(e => e.PermissionId)
                .HasConstraintName("FK_TblRolePermissionRel_TblPermission");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.TblRolePermissionRel)
                .HasForeignKey(e => e.RoleId)
                .HasConstraintName("FK_TblRolePermissionRel_TblRole");
        }
    }
}

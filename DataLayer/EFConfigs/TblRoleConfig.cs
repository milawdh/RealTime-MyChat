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
    public class TblRoleConfig : IEntityTypeConfiguration<TblRole>
    {
        public void Configure(EntityTypeBuilder<TblRole> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
        }
    }
}

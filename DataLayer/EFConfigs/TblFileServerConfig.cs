using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EFConfigs
{
    public class TblFileServerConfig : IEntityTypeConfiguration<TblFileServer>
    {
        public void Configure(EntityTypeBuilder<TblFileServer> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            builder.Property(e => e.IsActive).HasDefaultValue(true);
        }
    }
}

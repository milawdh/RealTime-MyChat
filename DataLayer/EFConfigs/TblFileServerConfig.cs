﻿using Domain.Entities;
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
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
        }
    }
}

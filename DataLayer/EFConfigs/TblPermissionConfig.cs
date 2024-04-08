﻿using Domain.Entities;
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
    internal class TblPermissionConfig : IEntityTypeConfiguration<TblPermission>
    {
        public void Configure(EntityTypeBuilder<TblPermission> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
        }
    }
}

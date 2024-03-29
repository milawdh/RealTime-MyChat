﻿using Domain.Models;
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
    public class TblUsersConfig : IEntityTypeConfiguration<TblUsers>
    {
        public void Configure(EntityTypeBuilder<TblUsers> builder)
        {
            builder.HasQueryFilter(q => !q.IsDeleted);

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            builder.Property(e => e.DateSigned).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.LastOnline).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.ProfileImageUrl).HasDefaultValue(new Guid("4c271239-f0c1-ee11-b6e1-44af2843979e"));
            builder.Property(e => e.RoleId).HasDefaultValue(new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e"));
            builder.Property(e => e.SettingsId).HasDefaultValue((short)1);

            builder.HasOne(d => d.ProfileImageUrlNavigation).WithMany(p => p.TblUsers)
                .HasForeignKey(e => e.ProfileImageUrl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUsers_TblImage");

            builder.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUsers_TblRole");

            builder.HasOne(d => d.Settings).WithMany(p => p.TblUsers)
                .HasForeignKey(e => e.SettingsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUsers_TblSettings");

            builder.HasOne(d => d.UserNameNavigation).WithMany(p => p.TblUsers)
                .HasForeignKey(e => e.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUsers_TblMyChatIdentifier");
        }
    }
}
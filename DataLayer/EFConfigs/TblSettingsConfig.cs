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
    public class TblSettingsConfig : IEntityTypeConfiguration<TblSetting>
    {
        public void Configure(EntityTypeBuilder<TblSetting> builder)
        {
            builder.Property(e => e.ShowPhoneNumber).HasComment("0 NoBody\r\n1 MyContacts\r\n2 EveryBody\r\n");
        }
    }
}

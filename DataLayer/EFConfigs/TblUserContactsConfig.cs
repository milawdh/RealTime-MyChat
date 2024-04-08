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
    public class TblUserContactsConfig : IEntityTypeConfiguration<TblUserContacts>
    {
        public void Configure(EntityTypeBuilder<TblUserContacts> builder)
        {
            builder.HasKey(table => new { table.ContactListOwnerId, table.ContactName });

            builder.HasOne(d => d.ContactListOwner).WithMany(p => p.TblUserContactsContactListOwner)
                .HasForeignKey(e => e.ContactListOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUserContacts_TblUsers");

            builder.HasOne(d => d.ContactUser).WithMany(p => p.TblUserContactsContactUser)
                .HasForeignKey(e => e.ContactUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUserContacts_TblUsers1");
        }
    }
}

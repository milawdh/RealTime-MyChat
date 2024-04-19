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
    public class TblUserChatRoomMapPermissionConfig : IEntityTypeConfiguration<TblUserChatRoomMapPermission>
    {
        public void Configure(EntityTypeBuilder<TblUserChatRoomMapPermission> builder)
        {

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(x => x.UserChatRoomRel)
                .WithMany(x => x.TblUserChatRoomMapPermissions)
                .HasForeignKey(x => x.UserChatRoomRelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TblUserChatRoomRel_TblUserChatRoomMapPermission");


            builder.HasOne(x => x.Permission)
                .WithMany(x => x.TblUserChatRoomMapPermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TblPermission_TblUserChatRoomMapPermission");
        }
    }
}

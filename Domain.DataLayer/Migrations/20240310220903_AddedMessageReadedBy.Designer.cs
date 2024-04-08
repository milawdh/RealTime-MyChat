﻿// <auto-generated />
using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.DataLayer.Migrations
{
    [DbContext(typeof(MyChatContext))]
    [Migration("20240310220903_AddedMessageReadedBy")]
    partial class AddedMessageReadedBy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.MessageReadByUserMap", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("MessageReadByUserMap");
                });

            modelBuilder.Entity("Domain.Entities.TblChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<string>("ChatRoomTitle")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MyChatId")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProfileImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("MyChatId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("TblChatRoom");
                });

            modelBuilder.Entity("Domain.Entities.TblImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("TblImage");
                });

            modelBuilder.Entity("Domain.Entities.TblMedia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("TblMedia");
                });

            modelBuilder.Entity("Domain.Entities.TblMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFromSystem")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<Guid>("RecieverChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReplyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SendAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RecieverChatRoomId");

                    b.HasIndex("ReplyId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("TblMessage");
                });

            modelBuilder.Entity("Domain.Entities.TblMyChatIdentifier", b =>
                {
                    b.Property<string>("Identifier")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Identifier");

                    b.ToTable("TblMyChatIdentifier");
                });

            modelBuilder.Entity("Domain.Entities.TblPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("TblPermission");
                });

            modelBuilder.Entity("Domain.Entities.TblRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<bool>("IsCostume")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("TblRole");
                });

            modelBuilder.Entity("Domain.Entities.TblRolePermissionRel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("TblRolePermissionRel");
                });

            modelBuilder.Entity("Domain.Entities.TblSettings", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<bool>("ShowLastSeen")
                        .HasColumnType("bit");

                    b.Property<bool>("ShowOnline")
                        .HasColumnType("bit");

                    b.Property<short>("ShowPhoneNumber")
                        .HasColumnType("smallint")
                        .HasComment("0 NoBody\r\n1 MyContacts\r\n2 EveryBody\r\n");

                    b.Property<bool>("ShowProfilePhoto")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TblSettings");
                });

            modelBuilder.Entity("Domain.Entities.TblUserChatRoomRel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("UserId");

                    b.ToTable("TblUserChatRoomRel");
                });

            modelBuilder.Entity("Domain.Entities.TblUserContacts", b =>
                {
                    b.Property<Guid>("ContactListOwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid>("ContactUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContactListOwnerId", "ContactName");

                    b.HasIndex("ContactUserId");

                    b.ToTable("TblUserContacts");
                });

            modelBuilder.Entity("Domain.Entities.TblUserImageRel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<Guid>("ImageUrl")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ImageUrl");

                    b.HasIndex("UserId");

                    b.ToTable("TblUserImageRel");
                });

            modelBuilder.Entity("Domain.Entities.TblUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<string>("Bio")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("DateSigned")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("LastOnline")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("ProfileImageUrl")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("4c271239-f0c1-ee11-b6e1-44af2843979e"));

                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e"));

                    b.Property<short>("SettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1);

                    b.Property<string>("Tell")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileImageUrl");

                    b.HasIndex("RoleId");

                    b.HasIndex("SettingsId");

                    b.HasIndex("UserName");

                    b.HasIndex(new[] { "Tell" }, "IX_TblUsers_Tell")
                        .IsUnique();

                    b.ToTable("TblUsers");
                });

            modelBuilder.Entity("Domain.Entities.MessageReadByUserMap", b =>
                {
                    b.HasOne("Domain.Entities.TblMessage", "Message")
                        .WithMany("ReadedBys")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TblUsers", "User")
                        .WithMany("ReadedMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.TblChatRoom", b =>
                {
                    b.HasOne("Domain.Entities.TblMyChatIdentifier", "MyChat")
                        .WithMany("TblChatRoom")
                        .HasForeignKey("MyChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_TblChatRoom_TblMyChatIdentifier");

                    b.HasOne("Domain.Entities.TblUsers", "Owner")
                        .WithMany("TblChatRoom")
                        .HasForeignKey("OwnerId")
                        .IsRequired()
                        .HasConstraintName("FK_TblChatRoom_TblUsers");

                    b.HasOne("Domain.Entities.TblChatRoom", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK_TblChatRoom_TblChatRoom");

                    b.HasOne("Domain.Entities.TblImage", "ProfileImage")
                        .WithMany("TblChatRoom")
                        .HasForeignKey("ProfileImageId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_TblChatRoom_TblImage");

                    b.Navigation("MyChat");

                    b.Navigation("Owner");

                    b.Navigation("Parent");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("Domain.Entities.TblMedia", b =>
                {
                    b.HasOne("Domain.Entities.TblMessage", "Message")
                        .WithMany("TblMedia")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TblMedia_TblMessage");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Domain.Entities.TblMessage", b =>
                {
                    b.HasOne("Domain.Entities.TblChatRoom", "RecieverChatRoom")
                        .WithMany("TblMessage")
                        .HasForeignKey("RecieverChatRoomId")
                        .IsRequired()
                        .HasConstraintName("FK_TblMessage_TblChatRoom");

                    b.HasOne("Domain.Entities.TblMessage", "Reply")
                        .WithMany("InverseReply")
                        .HasForeignKey("ReplyId")
                        .HasConstraintName("FK_TblMessage_TblMessage");

                    b.HasOne("Domain.Entities.TblUsers", "SenderUser")
                        .WithMany("TblMessage")
                        .HasForeignKey("SenderUserId")
                        .IsRequired()
                        .HasConstraintName("FK_TblMessage_TblUsers");

                    b.Navigation("RecieverChatRoom");

                    b.Navigation("Reply");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("Domain.Entities.TblRolePermissionRel", b =>
                {
                    b.HasOne("Domain.Entities.TblPermission", "Permission")
                        .WithMany("TblRolePermissionRel")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TblRolePermissionRel_TblPermission");

                    b.HasOne("Domain.Entities.TblRole", "Role")
                        .WithMany("TblRolePermissionRel")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TblRolePermissionRel_TblRole");

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.TblUserChatRoomRel", b =>
                {
                    b.HasOne("Domain.Entities.TblChatRoom", "ChatRoom")
                        .WithMany("TblUserChatRoomRel")
                        .HasForeignKey("ChatRoomId")
                        .IsRequired()
                        .HasConstraintName("FK_TblUserChatRoomRel_TblChatRoom");

                    b.HasOne("Domain.Entities.TblUsers", "User")
                        .WithMany("TblUserChatRoomRel")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_TblUserChatRoomRel_TblUsers");

                    b.Navigation("ChatRoom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.TblUserContacts", b =>
                {
                    b.HasOne("Domain.Entities.TblUsers", "ContactListOwner")
                        .WithMany("TblUserContactsContactListOwner")
                        .HasForeignKey("ContactListOwnerId")
                        .IsRequired()
                        .HasConstraintName("FK_TblUserContacts_TblUsers");

                    b.HasOne("Domain.Entities.TblUsers", "ContactUser")
                        .WithMany("TblUserContactsContactUser")
                        .HasForeignKey("ContactUserId")
                        .IsRequired()
                        .HasConstraintName("FK_TblUserContacts_TblUsers1");

                    b.Navigation("ContactListOwner");

                    b.Navigation("ContactUser");
                });

            modelBuilder.Entity("Domain.Entities.TblUserImageRel", b =>
                {
                    b.HasOne("Domain.Entities.TblImage", "ImageUrlNavigation")
                        .WithMany("TblUserImageRel")
                        .HasForeignKey("ImageUrl")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TblUserImageRel_TblImage");

                    b.HasOne("Domain.Entities.TblUsers", "User")
                        .WithMany("TblUserImageRel")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TblUserImageRel_TblUsers");

                    b.Navigation("ImageUrlNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.TblUsers", b =>
                {
                    b.HasOne("Domain.Entities.TblImage", "ProfileImageUrlNavigation")
                        .WithMany("TblUsers")
                        .HasForeignKey("ProfileImageUrl")
                        .IsRequired()
                        .HasConstraintName("FK_TblUsers_TblImage");

                    b.HasOne("Domain.Entities.TblRole", "Role")
                        .WithMany("TblUsers")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_TblUsers_TblRole");

                    b.HasOne("Domain.Entities.TblSettings", "Settings")
                        .WithMany("TblUsers")
                        .HasForeignKey("SettingsId")
                        .IsRequired()
                        .HasConstraintName("FK_TblUsers_TblSettings");

                    b.HasOne("Domain.Entities.TblMyChatIdentifier", "UserNameNavigation")
                        .WithMany("TblUsers")
                        .HasForeignKey("UserName")
                        .IsRequired()
                        .HasConstraintName("FK_TblUsers_TblMyChatIdentifier");

                    b.Navigation("ProfileImageUrlNavigation");

                    b.Navigation("Role");

                    b.Navigation("Settings");

                    b.Navigation("UserNameNavigation");
                });

            modelBuilder.Entity("Domain.Entities.TblChatRoom", b =>
                {
                    b.Navigation("InverseParent");

                    b.Navigation("TblMessage");

                    b.Navigation("TblUserChatRoomRel");
                });

            modelBuilder.Entity("Domain.Entities.TblImage", b =>
                {
                    b.Navigation("TblChatRoom");

                    b.Navigation("TblUserImageRel");

                    b.Navigation("TblUsers");
                });

            modelBuilder.Entity("Domain.Entities.TblMessage", b =>
                {
                    b.Navigation("InverseReply");

                    b.Navigation("ReadedBys");

                    b.Navigation("TblMedia");
                });

            modelBuilder.Entity("Domain.Entities.TblMyChatIdentifier", b =>
                {
                    b.Navigation("TblChatRoom");

                    b.Navigation("TblUsers");
                });

            modelBuilder.Entity("Domain.Entities.TblPermission", b =>
                {
                    b.Navigation("TblRolePermissionRel");
                });

            modelBuilder.Entity("Domain.Entities.TblRole", b =>
                {
                    b.Navigation("TblRolePermissionRel");

                    b.Navigation("TblUsers");
                });

            modelBuilder.Entity("Domain.Entities.TblSettings", b =>
                {
                    b.Navigation("TblUsers");
                });

            modelBuilder.Entity("Domain.Entities.TblUsers", b =>
                {
                    b.Navigation("ReadedMessages");

                    b.Navigation("TblChatRoom");

                    b.Navigation("TblMessage");

                    b.Navigation("TblUserChatRoomRel");

                    b.Navigation("TblUserContactsContactListOwner");

                    b.Navigation("TblUserContactsContactUser");

                    b.Navigation("TblUserImageRel");
                });
#pragma warning restore 612, 618
        }
    }
}
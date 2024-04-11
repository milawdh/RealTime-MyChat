using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class TestMessage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUsers",
                table: "TblMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUsers_CreatedById",
                table: "TblMessage");

            migrationBuilder.DropIndex(
                name: "IX_TblMessage_SenderUserId",
                table: "TblMessage");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "TblMessage");

            migrationBuilder.DropColumn(
                name: "SendAt",
                table: "TblMessage");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "TblMessage");

            migrationBuilder.RenameColumn(
                name: "ContactListOwnerId",
                table: "TblUserContacts",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "TblMedia",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TblChatRoom",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TblChatRoom_OwnerId",
                table: "TblChatRoom",
                newName: "IX_TblChatRoom_CreatedById");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblUserImageRel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblUserImageRel",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblUserImageRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblUserImageRel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblUserImageRel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblUserImageRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblUserImageRel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblUserContacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblUserContacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblUserContacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblUserChatRoomRel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblUserChatRoomRel",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblUserChatRoomRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblUserChatRoomRel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblUserChatRoomRel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblUserChatRoomRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblUserChatRoomRel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblSettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblSettings",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblSettings",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblRolePermissionRel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblRole",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblRole",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblRole",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblRole",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblRole",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblPermission",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblPermission",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblPermission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblPermission",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblMessage",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "TblMessage",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TblMessage",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblMedia",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblMedia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblMedia",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblMedia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblMedia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblMedia",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblImage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblImage",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblImage",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblImage",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblImage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblImage",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblImage",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblChatRoom",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblChatRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblChatRoom",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblChatRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblChatRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblChatRoom",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_CreatedById",
                table: "TblUserImageRel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_DeleteById",
                table: "TblUserImageRel",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_ModifiedById",
                table: "TblUserImageRel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserContacts_ModifiedById",
                table: "TblUserContacts",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_CreatedById",
                table: "TblUserChatRoomRel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_DeleteById",
                table: "TblUserChatRoomRel",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_ModifiedById",
                table: "TblUserChatRoomRel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblSettings_CreatedById",
                table: "TblSettings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblSettings_DeleteById",
                table: "TblSettings",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblSettings_ModifiedById",
                table: "TblSettings",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblRole_CreatedById",
                table: "TblRole",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblRole_DeleteById",
                table: "TblRole",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblRole_ModifiedById",
                table: "TblRole",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblPermission_CreatedById",
                table: "TblPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblPermission_DeleteById",
                table: "TblPermission",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblPermission_ModifiedById",
                table: "TblPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_CreatedById",
                table: "TblMedia",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_DeleteById",
                table: "TblMedia",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_ModifiedById",
                table: "TblMedia",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblImage_CreatedById",
                table: "TblImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblImage_DeleteById",
                table: "TblImage",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblImage_ModifiedById",
                table: "TblImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_DeleteById",
                table: "TblChatRoom",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_ModifiedById",
                table: "TblChatRoom",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblUsers_DeleteById",
                table: "TblChatRoom",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblUsers_ModifiedById",
                table: "TblChatRoom",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblImage_TblUsers_CreatedById",
                table: "TblImage",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblImage_TblUsers_DeleteById",
                table: "TblImage",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblImage_TblUsers_ModifiedById",
                table: "TblImage",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMedia_TblUsers_CreatedById",
                table: "TblMedia",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMedia_TblUsers_DeleteById",
                table: "TblMedia",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMedia_TblUsers_ModifiedById",
                table: "TblMedia",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUsers",
                table: "TblMessage",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUsers_CreatedById",
                table: "TblPermission",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUsers_DeleteById",
                table: "TblPermission",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUsers_ModifiedById",
                table: "TblPermission",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRole_TblUsers_CreatedById",
                table: "TblRole",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRole_TblUsers_DeleteById",
                table: "TblRole",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRole_TblUsers_ModifiedById",
                table: "TblRole",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblSettings_TblUsers_CreatedById",
                table: "TblSettings",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblSettings_TblUsers_DeleteById",
                table: "TblSettings",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblSettings_TblUsers_ModifiedById",
                table: "TblSettings",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers_CreatedById",
                table: "TblUserChatRoomRel",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers_DeleteById",
                table: "TblUserChatRoomRel",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers_ModifiedById",
                table: "TblUserChatRoomRel",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserContacts_TblUsers_ModifiedById",
                table: "TblUserContacts",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUsers_CreatedById",
                table: "TblUserImageRel",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUsers_DeleteById",
                table: "TblUserImageRel",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUsers_ModifiedById",
                table: "TblUserImageRel",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblChatRoom_TblUsers_DeleteById",
                table: "TblChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_TblChatRoom_TblUsers_ModifiedById",
                table: "TblChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_TblImage_TblUsers_CreatedById",
                table: "TblImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblImage_TblUsers_DeleteById",
                table: "TblImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblImage_TblUsers_ModifiedById",
                table: "TblImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMedia_TblUsers_CreatedById",
                table: "TblMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMedia_TblUsers_DeleteById",
                table: "TblMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMedia_TblUsers_ModifiedById",
                table: "TblMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUsers",
                table: "TblMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUsers_CreatedById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUsers_DeleteById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUsers_ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRole_TblUsers_CreatedById",
                table: "TblRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRole_TblUsers_DeleteById",
                table: "TblRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRole_TblUsers_ModifiedById",
                table: "TblRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSettings_TblUsers_CreatedById",
                table: "TblSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSettings_TblUsers_DeleteById",
                table: "TblSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSettings_TblUsers_ModifiedById",
                table: "TblSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers_CreatedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers_DeleteById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers_ModifiedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserContacts_TblUsers_ModifiedById",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers_CreatedById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers_DeleteById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers_ModifiedById",
                table: "TblUserImageRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserImageRel_CreatedById",
                table: "TblUserImageRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserImageRel_DeleteById",
                table: "TblUserImageRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserImageRel_ModifiedById",
                table: "TblUserImageRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserContacts_ModifiedById",
                table: "TblUserContacts");

            migrationBuilder.DropIndex(
                name: "IX_TblUserChatRoomRel_CreatedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserChatRoomRel_DeleteById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserChatRoomRel_ModifiedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropIndex(
                name: "IX_TblSettings_CreatedById",
                table: "TblSettings");

            migrationBuilder.DropIndex(
                name: "IX_TblSettings_DeleteById",
                table: "TblSettings");

            migrationBuilder.DropIndex(
                name: "IX_TblSettings_ModifiedById",
                table: "TblSettings");

            migrationBuilder.DropIndex(
                name: "IX_TblRole_CreatedById",
                table: "TblRole");

            migrationBuilder.DropIndex(
                name: "IX_TblRole_DeleteById",
                table: "TblRole");

            migrationBuilder.DropIndex(
                name: "IX_TblRole_ModifiedById",
                table: "TblRole");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_CreatedById",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_DeleteById",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblMedia_CreatedById",
                table: "TblMedia");

            migrationBuilder.DropIndex(
                name: "IX_TblMedia_DeleteById",
                table: "TblMedia");

            migrationBuilder.DropIndex(
                name: "IX_TblMedia_ModifiedById",
                table: "TblMedia");

            migrationBuilder.DropIndex(
                name: "IX_TblImage_CreatedById",
                table: "TblImage");

            migrationBuilder.DropIndex(
                name: "IX_TblImage_DeleteById",
                table: "TblImage");

            migrationBuilder.DropIndex(
                name: "IX_TblImage_ModifiedById",
                table: "TblImage");

            migrationBuilder.DropIndex(
                name: "IX_TblChatRoom_DeleteById",
                table: "TblChatRoom");

            migrationBuilder.DropIndex(
                name: "IX_TblChatRoom_ModifiedById",
                table: "TblChatRoom");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblUserImageRel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblUserContacts");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblUserContacts");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblUserContacts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblSettings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblRole");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblImage");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblChatRoom");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblChatRoom");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblChatRoom");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblChatRoom");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblChatRoom");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblChatRoom");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TblUserContacts",
                newName: "ContactListOwnerId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "TblMedia",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TblChatRoom",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_TblChatRoom_CreatedById",
                table: "TblChatRoom",
                newName: "IX_TblChatRoom_OwnerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblMessage",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "TblMessage",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TblMessage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "TblMessage",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendAt",
                table: "TblMessage",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderUserId",
                table: "TblMessage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_SenderUserId",
                table: "TblMessage",
                column: "SenderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUsers",
                table: "TblMessage",
                column: "SenderUserId",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUsers_CreatedById",
                table: "TblMessage",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");
        }
    }
}

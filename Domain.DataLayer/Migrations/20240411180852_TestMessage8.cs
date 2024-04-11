using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class TestMessage8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblChatRoom_TblUsers",
                table: "TblChatRoom");

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
                name: "FK_TblMessage_TblUsers_DeleteById",
                table: "TblMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUsers_ModifiedById",
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
                name: "FK_TblRolePermissionRel_TblUsers_CreatedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_DeleteById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_ModifiedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers",
                table: "TblUserChatRoomRel");

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
                name: "FK_TblUserContacts_TblUsers",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserContacts_TblUsers1",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserContacts_TblUsers_ModifiedById",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers_CreatedById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers_DeleteById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers_ModifiedById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSettings_TblUsers_CreatedById",
                table: "TblSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSettings_TblUsers_DeleteById",
                table: "TblSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSettings_TblUsers_ModifiedById",
                table: "TblSettings");

            migrationBuilder.DropTable(
                name: "TblUsers");

            migrationBuilder.DropTable(
                name: "TblSettings");

            migrationBuilder.CreateTable(
                name: "TblSetting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowOnline = table.Column<bool>(type: "bit", nullable: false),
                    ShowLastSeen = table.Column<bool>(type: "bit", nullable: false),
                    ShowProfilePhoto = table.Column<bool>(type: "bit", nullable: false),
                    ShowPhoneNumber = table.Column<short>(type: "smallint", nullable: false, comment: "0 NoBody\r\n1 MyContacts\r\n2 EveryBody\r\n"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeleteById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    UserName = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Tell = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateSigned = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastOnline = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ProfileImageUrl = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("4c271239-f0c1-ee11-b6e1-44af2843979e")),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e")),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    SettingId = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblUsers_TblImage",
                        column: x => x.ProfileImageUrl,
                        principalTable: "TblImage",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUsers_TblMyChatIdentifier",
                        column: x => x.UserName,
                        principalTable: "TblMyChatIdentifier",
                        principalColumn: "Identifier");
                    table.ForeignKey(
                        name: "FK_TblUsers_TblRole",
                        column: x => x.RoleId,
                        principalTable: "TblRole",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUsers_TblSettings",
                        column: x => x.SettingId,
                        principalTable: "TblSetting",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblSetting_CreatedById",
                table: "TblSetting",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblSetting_DeleteById",
                table: "TblSetting",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblSetting_ModifiedById",
                table: "TblSetting",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_ProfileImageUrl",
                table: "TblUser",
                column: "ProfileImageUrl");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_RoleId",
                table: "TblUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_SettingId",
                table: "TblUser",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_UserName",
                table: "TblUser",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsers_Tell",
                table: "TblUser",
                column: "Tell",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblUser_DeleteById",
                table: "TblChatRoom",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblUser_ModifiedById",
                table: "TblChatRoom",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblUsers",
                table: "TblChatRoom",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblImage_TblUser_CreatedById",
                table: "TblImage",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblImage_TblUser_DeleteById",
                table: "TblImage",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblImage_TblUser_ModifiedById",
                table: "TblImage",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMedia_TblUser_CreatedById",
                table: "TblMedia",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMedia_TblUser_DeleteById",
                table: "TblMedia",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMedia_TblUser_ModifiedById",
                table: "TblMedia",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUser_DeleteById",
                table: "TblMessage",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUser_ModifiedById",
                table: "TblMessage",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUsers",
                table: "TblMessage",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUser_CreatedById",
                table: "TblPermission",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUser_DeleteById",
                table: "TblPermission",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUser_ModifiedById",
                table: "TblPermission",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRole_TblUser_CreatedById",
                table: "TblRole",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRole_TblUser_DeleteById",
                table: "TblRole",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRole_TblUser_ModifiedById",
                table: "TblRole",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRolePermissionRel_TblUser_CreatedById",
                table: "TblRolePermissionRel",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRolePermissionRel_TblUser_DeleteById",
                table: "TblRolePermissionRel",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRolePermissionRel_TblUser_ModifiedById",
                table: "TblRolePermissionRel",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUser_CreatedById",
                table: "TblUserChatRoomRel",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUser_DeleteById",
                table: "TblUserChatRoomRel",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUser_ModifiedById",
                table: "TblUserChatRoomRel",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers",
                table: "TblUserChatRoomRel",
                column: "UserId",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserContacts_TblUser_ModifiedById",
                table: "TblUserContacts",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserContacts_TblUsers",
                table: "TblUserContacts",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserContacts_TblUsers1",
                table: "TblUserContacts",
                column: "ContactUserId",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUser_CreatedById",
                table: "TblUserImageRel",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUser_DeleteById",
                table: "TblUserImageRel",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUser_ModifiedById",
                table: "TblUserImageRel",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUsers",
                table: "TblUserImageRel",
                column: "UserId",
                principalTable: "TblUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblSetting_TblUser_CreatedById",
                table: "TblSetting",
                column: "CreatedById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblSetting_TblUser_DeleteById",
                table: "TblSetting",
                column: "DeleteById",
                principalTable: "TblUser",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblSetting_TblUser_ModifiedById",
                table: "TblSetting",
                column: "ModifiedById",
                principalTable: "TblUser",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblChatRoom_TblUser_DeleteById",
                table: "TblChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_TblChatRoom_TblUser_ModifiedById",
                table: "TblChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_TblChatRoom_TblUsers",
                table: "TblChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_TblImage_TblUser_CreatedById",
                table: "TblImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblImage_TblUser_DeleteById",
                table: "TblImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblImage_TblUser_ModifiedById",
                table: "TblImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMedia_TblUser_CreatedById",
                table: "TblMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMedia_TblUser_DeleteById",
                table: "TblMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMedia_TblUser_ModifiedById",
                table: "TblMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUser_DeleteById",
                table: "TblMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUser_ModifiedById",
                table: "TblMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblMessage_TblUsers",
                table: "TblMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUser_CreatedById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUser_DeleteById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUser_ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRole_TblUser_CreatedById",
                table: "TblRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRole_TblUser_DeleteById",
                table: "TblRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRole_TblUser_ModifiedById",
                table: "TblRole");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUser_CreatedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUser_DeleteById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUser_ModifiedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUser_CreatedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUser_DeleteById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUser_ModifiedById",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserContacts_TblUser_ModifiedById",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserContacts_TblUsers",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserContacts_TblUsers1",
                table: "TblUserContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUser_CreatedById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUser_DeleteById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUser_ModifiedById",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserImageRel_TblUsers",
                table: "TblUserImageRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSetting_TblUser_CreatedById",
                table: "TblSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSetting_TblUser_DeleteById",
                table: "TblSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSetting_TblUser_ModifiedById",
                table: "TblSetting");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblSetting");

            migrationBuilder.CreateTable(
                name: "TblSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShowLastSeen = table.Column<bool>(type: "bit", nullable: false),
                    ShowOnline = table.Column<bool>(type: "bit", nullable: false),
                    ShowPhoneNumber = table.Column<short>(type: "smallint", nullable: false, comment: "0 NoBody\r\n1 MyContacts\r\n2 EveryBody\r\n"),
                    ShowProfilePhoto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSettings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ProfileImageUrl = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("4c271239-f0c1-ee11-b6e1-44af2843979e")),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e")),
                    SettingsId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UserName = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSigned = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastOnline = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    Tell = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblUsers_TblImage",
                        column: x => x.ProfileImageUrl,
                        principalTable: "TblImage",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUsers_TblMyChatIdentifier",
                        column: x => x.UserName,
                        principalTable: "TblMyChatIdentifier",
                        principalColumn: "Identifier");
                    table.ForeignKey(
                        name: "FK_TblUsers_TblRole",
                        column: x => x.RoleId,
                        principalTable: "TblRole",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUsers_TblSettings",
                        column: x => x.SettingsId,
                        principalTable: "TblSettings",
                        principalColumn: "ID");
                });

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
                name: "IX_TblUsers_ProfileImageUrl",
                table: "TblUsers",
                column: "ProfileImageUrl");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsers_RoleId",
                table: "TblUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsers_SettingsId",
                table: "TblUsers",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsers_Tell",
                table: "TblUsers",
                column: "Tell",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblUsers_UserName",
                table: "TblUsers",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblUsers",
                table: "TblChatRoom",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

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
                name: "FK_TblMessage_TblUsers_DeleteById",
                table: "TblMessage",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblMessage_TblUsers_ModifiedById",
                table: "TblMessage",
                column: "ModifiedById",
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
                name: "FK_TblRolePermissionRel_TblUsers_CreatedById",
                table: "TblRolePermissionRel",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_DeleteById",
                table: "TblRolePermissionRel",
                column: "DeleteById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_ModifiedById",
                table: "TblRolePermissionRel",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUsers",
                table: "TblUserChatRoomRel",
                column: "UserId",
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
                name: "FK_TblUserContacts_TblUsers",
                table: "TblUserContacts",
                column: "CreatedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserContacts_TblUsers1",
                table: "TblUserContacts",
                column: "ContactUserId",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserContacts_TblUsers_ModifiedById",
                table: "TblUserContacts",
                column: "ModifiedById",
                principalTable: "TblUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserImageRel_TblUsers",
                table: "TblUserImageRel",
                column: "UserId",
                principalTable: "TblUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
        }
    }
}

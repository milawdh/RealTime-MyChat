using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitMainDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblMyChatIdentifier",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMyChatIdentifier", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "TblChatRoom",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    MyChatId = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    ChatRoomTitle = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ProfileImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeleteById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblChatRoom", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblChatRoom_TblChatRoom",
                        column: x => x.ParentId,
                        principalTable: "TblChatRoom",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblChatRoom_TblMyChatIdentifier",
                        column: x => x.MyChatId,
                        principalTable: "TblMyChatIdentifier",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblImage",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    table.PrimaryKey("PK_TblImage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblMedia",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeleteById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMedia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblMessage",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    RecieverChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFromSystem = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeleteById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMessage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblMessage_TblChatRoom",
                        column: x => x.RecieverChatRoomId,
                        principalTable: "TblChatRoom",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblMessage_TblMessage",
                        column: x => x.ReplyId,
                        principalTable: "TblMessage",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblPermission",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_TblPermission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblRole",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsCostume = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_TblRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblRolePermissionRel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_TblRolePermissionRel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblRolePermissionRel_TblPermission",
                        column: x => x.PermissionId,
                        principalTable: "TblPermission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblRolePermissionRel_TblRole",
                        column: x => x.RoleId,
                        principalTable: "TblRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "TblUserChatRoomRel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastSeenMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_TblUserChatRoomRel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblChatRoom",
                        column: x => x.ChatRoomId,
                        principalTable: "TblChatRoom",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                        column: x => x.LastSeenMessageId,
                        principalTable: "TblMessage",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblUser_DeleteById",
                        column: x => x.DeleteById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblUser_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblUsers",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblUserContacts",
                columns: table => new
                {
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ContactUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserContacts", x => new { x.CreatedById, x.ContactName });
                    table.ForeignKey(
                        name: "FK_TblUserContacts_TblUser_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserContacts_TblUsers",
                        column: x => x.CreatedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserContacts_TblUsers1",
                        column: x => x.ContactUserId,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblUserImageRel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ImageUrl = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_TblUserImageRel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblUserImageRel_TblImage",
                        column: x => x.ImageUrl,
                        principalTable: "TblImage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblUserImageRel_TblUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserImageRel_TblUser_DeleteById",
                        column: x => x.DeleteById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserImageRel_TblUser_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserImageRel_TblUsers",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_CreatedById",
                table: "TblChatRoom",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_DeleteById",
                table: "TblChatRoom",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_ModifiedById",
                table: "TblChatRoom",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_MyChatId",
                table: "TblChatRoom",
                column: "MyChatId");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_ParentId",
                table: "TblChatRoom",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_ProfileImageId",
                table: "TblChatRoom",
                column: "ProfileImageId");

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
                name: "IX_TblMedia_CreatedById",
                table: "TblMedia",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_DeleteById",
                table: "TblMedia",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_MessageId",
                table: "TblMedia",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_ModifiedById",
                table: "TblMedia",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_CreatedById",
                table: "TblMessage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_DeleteById",
                table: "TblMessage",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_ModifiedById",
                table: "TblMessage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_RecieverChatRoomId",
                table: "TblMessage",
                column: "RecieverChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_ReplyId",
                table: "TblMessage",
                column: "ReplyId");

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
                name: "IX_TblRolePermissionRel_CreatedById",
                table: "TblRolePermissionRel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblRolePermissionRel_DeleteById",
                table: "TblRolePermissionRel",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblRolePermissionRel_ModifiedById",
                table: "TblRolePermissionRel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblRolePermissionRel_PermissionId",
                table: "TblRolePermissionRel",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TblRolePermissionRel_RoleId",
                table: "TblRolePermissionRel",
                column: "RoleId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_ChatRoomId",
                table: "TblUserChatRoomRel",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_CreatedById",
                table: "TblUserChatRoomRel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_DeleteById",
                table: "TblUserChatRoomRel",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_LastSeenMessageId",
                table: "TblUserChatRoomRel",
                column: "LastSeenMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_ModifiedById",
                table: "TblUserChatRoomRel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_UserId",
                table: "TblUserChatRoomRel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserContacts_ContactUserId",
                table: "TblUserContacts",
                column: "ContactUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserContacts_ModifiedById",
                table: "TblUserContacts",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_CreatedById",
                table: "TblUserImageRel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_DeleteById",
                table: "TblUserImageRel",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_ImageUrl",
                table: "TblUserImageRel",
                column: "ImageUrl");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_ModifiedById",
                table: "TblUserImageRel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_UserId",
                table: "TblUserImageRel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblChatRoom_TblImage",
                table: "TblChatRoom",
                column: "ProfileImageId",
                principalTable: "TblImage",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_TblMedia_TblMessage",
                table: "TblMedia",
                column: "MessageId",
                principalTable: "TblMessage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_TblUsers_TblImage",
                table: "TblUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUsers_TblMyChatIdentifier",
                table: "TblUser");

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
                name: "FK_TblSetting_TblUser_CreatedById",
                table: "TblSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSetting_TblUser_DeleteById",
                table: "TblSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_TblSetting_TblUser_ModifiedById",
                table: "TblSetting");

            migrationBuilder.DropTable(
                name: "TblMedia");

            migrationBuilder.DropTable(
                name: "TblRolePermissionRel");

            migrationBuilder.DropTable(
                name: "TblUserChatRoomRel");

            migrationBuilder.DropTable(
                name: "TblUserContacts");

            migrationBuilder.DropTable(
                name: "TblUserImageRel");

            migrationBuilder.DropTable(
                name: "TblPermission");

            migrationBuilder.DropTable(
                name: "TblMessage");

            migrationBuilder.DropTable(
                name: "TblChatRoom");

            migrationBuilder.DropTable(
                name: "TblImage");

            migrationBuilder.DropTable(
                name: "TblMyChatIdentifier");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblRole");

            migrationBuilder.DropTable(
                name: "TblSetting");
        }
    }
}

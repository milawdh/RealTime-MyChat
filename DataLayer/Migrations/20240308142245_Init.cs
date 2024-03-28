using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblImage",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblImage", x => x.ID);
                });

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
                name: "TblPermission",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
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
                    IsCostume = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblSettings",
                columns: table => new
                {
                    ID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowOnline = table.Column<bool>(type: "bit", nullable: false),
                    ShowLastSeen = table.Column<bool>(type: "bit", nullable: false),
                    ShowProfilePhoto = table.Column<bool>(type: "bit", nullable: false),
                    ShowPhoneNumber = table.Column<short>(type: "smallint", nullable: false, comment: "0 NoBody\r\n1 MyContacts\r\n2 EveryBody\r\n")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSettings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblRolePermissionRel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "TblUsers",
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
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e")),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    SettingsId = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1)
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

            migrationBuilder.CreateTable(
                name: "TblChatRoom",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    MyChatId = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    ChatRoomTitle = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ProfileImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_TblChatRoom_TblImage",
                        column: x => x.ProfileImageId,
                        principalTable: "TblImage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TblChatRoom_TblMyChatIdentifier",
                        column: x => x.MyChatId,
                        principalTable: "TblMyChatIdentifier",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblChatRoom_TblUsers",
                        column: x => x.OwnerId,
                        principalTable: "TblUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblUserContacts",
                columns: table => new
                {
                    ContactListOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ContactUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserContacts", x => new { x.ContactListOwnerId, x.ContactName });
                    table.ForeignKey(
                        name: "FK_TblUserContacts_TblUsers",
                        column: x => x.ContactListOwnerId,
                        principalTable: "TblUsers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserContacts_TblUsers1",
                        column: x => x.ContactUserId,
                        principalTable: "TblUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblUserImageRel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ImageUrl = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_TblUserImageRel_TblUsers",
                        column: x => x.UserId,
                        principalTable: "TblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblMessage",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecieverChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    EditedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsFromSystem = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_TblMessage_TblUsers",
                        column: x => x.SenderUserId,
                        principalTable: "TblUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblUserChatRoomRel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_TblUserChatRoomRel_TblUsers",
                        column: x => x.UserId,
                        principalTable: "TblUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TblMedia",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMedia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblMedia_TblMessage",
                        column: x => x.MessageId,
                        principalTable: "TblMessage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_MyChatId",
                table: "TblChatRoom",
                column: "MyChatId");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_OwnerId",
                table: "TblChatRoom",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_ParentId",
                table: "TblChatRoom",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TblChatRoom_ProfileImageId",
                table: "TblChatRoom",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_MessageId",
                table: "TblMedia",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_RecieverChatRoomId",
                table: "TblMessage",
                column: "RecieverChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_ReplyId",
                table: "TblMessage",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_TblMessage_SenderUserId",
                table: "TblMessage",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblRolePermissionRel_PermissionId",
                table: "TblRolePermissionRel",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TblRolePermissionRel_RoleId",
                table: "TblRolePermissionRel",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_ChatRoomId",
                table: "TblUserChatRoomRel",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_UserId",
                table: "TblUserChatRoomRel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserContacts_ContactUserId",
                table: "TblUserContacts",
                column: "ContactUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_ImageUrl",
                table: "TblUserImageRel",
                column: "ImageUrl");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserImageRel_UserId",
                table: "TblUserImageRel",
                column: "UserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "TblMessage");

            migrationBuilder.DropTable(
                name: "TblPermission");

            migrationBuilder.DropTable(
                name: "TblChatRoom");

            migrationBuilder.DropTable(
                name: "TblUsers");

            migrationBuilder.DropTable(
                name: "TblImage");

            migrationBuilder.DropTable(
                name: "TblMyChatIdentifier");

            migrationBuilder.DropTable(
                name: "TblRole");

            migrationBuilder.DropTable(
                name: "TblSettings");
        }
    }
}

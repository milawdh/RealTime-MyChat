using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class ReadedByToJsonField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReadByUserMap");

            migrationBuilder.AddColumn<string>(
                name: "ReadedByList",
                table: "TblMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadedByList",
                table: "TblMessage");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "TblMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MessageReadByUserMap",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReadByUserMap", x => new { x.UserId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_MessageReadByUserMap_TblMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "TblMessage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReadByUserMap_TblUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "TblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadByUserMap_MessageId",
                table: "MessageReadByUserMap",
                column: "MessageId");
        }
    }
}

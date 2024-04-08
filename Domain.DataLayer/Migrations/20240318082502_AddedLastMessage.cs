using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadedByList",
                table: "TblMessage");

            migrationBuilder.AddColumn<Guid>(
                name: "LastSeenMessageId",
                table: "TblUserChatRoomRel",
                type: "uniqueidentifier",
                nullable: true
                );

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomRel_LastSeenMessageId",
                table: "TblUserChatRoomRel",
                column: "LastSeenMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                table: "TblUserChatRoomRel",
                column: "LastSeenMessageId",
                principalTable: "TblMessage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropIndex(
                name: "IX_TblUserChatRoomRel_LastSeenMessageId",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "LastSeenMessageId",
                table: "TblUserChatRoomRel");

            migrationBuilder.AddColumn<string>(
                name: "ReadedByList",
                table: "TblMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

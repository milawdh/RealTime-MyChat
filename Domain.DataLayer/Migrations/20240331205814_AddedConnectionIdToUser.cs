using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedConnectionIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                table: "TblUserChatRoomRel");

            migrationBuilder.AlterColumn<int>(
                name: "SettingsId",
                table: "TblUsers",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldDefaultValue: (short)1);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "TblUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LastSeenMessageId",
                table: "TblUserChatRoomRel",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "TblSettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                table: "TblUserChatRoomRel",
                column: "LastSeenMessageId",
                principalTable: "TblMessage",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                table: "TblUserChatRoomRel");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "TblUsers");

            migrationBuilder.AlterColumn<short>(
                name: "SettingsId",
                table: "TblUsers",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "LastSeenMessageId",
                table: "TblUserChatRoomRel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ID",
                table: "TblSettings",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblMessage_LastSeenMessageId",
                table: "TblUserChatRoomRel",
                column: "LastSeenMessageId",
                principalTable: "TblMessage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

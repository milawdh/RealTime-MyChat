using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSoftDeleteFromTblPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TblPermission");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission",
                column: "PermissionId",
                principalTable: "TblPermission",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission",
                column: "UserChatRoomRelId",
                principalTable: "TblUserChatRoomRel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblUserChatRoomRel_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TblPermission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission",
                column: "PermissionId",
                principalTable: "TblPermission",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblUserChatRoomRel_TblUserChatRoomMapPermission",
                table: "TblUserChatRoomMapPermission",
                column: "UserChatRoomRelId",
                principalTable: "TblUserChatRoomRel",
                principalColumn: "ID");
        }
    }
}

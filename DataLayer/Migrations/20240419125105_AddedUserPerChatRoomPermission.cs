using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserPerChatRoomPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblUserChatRoomMapPermission",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    UserChatRoomRelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_TblUserChatRoomMapPermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblPermission_TblUserChatRoomMapPermission",
                        column: x => x.PermissionId,
                        principalTable: "TblPermission",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomMapPermission_TblUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomMapPermission_TblUser_DeleteById",
                        column: x => x.DeleteById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomMapPermission_TblUser_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblUserChatRoomRel_TblUserChatRoomMapPermission",
                        column: x => x.UserChatRoomRelId,
                        principalTable: "TblUserChatRoomRel",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomMapPermission_CreatedById",
                table: "TblUserChatRoomMapPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomMapPermission_DeleteById",
                table: "TblUserChatRoomMapPermission",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomMapPermission_ModifiedById",
                table: "TblUserChatRoomMapPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomMapPermission_PermissionId",
                table: "TblUserChatRoomMapPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserChatRoomMapPermission_UserChatRoomRelId",
                table: "TblUserChatRoomMapPermission",
                column: "UserChatRoomRelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblUserChatRoomMapPermission");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedParentIdToPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblPermission_ParentId",
                table: "TblPermission",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermission_Parent",
                table: "TblPermission",
                column: "ParentId",
                principalTable: "TblPermission",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_Parent",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_ParentId",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TblPermission");
        }
    }
}

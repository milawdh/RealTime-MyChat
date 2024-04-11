using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class TestMessage3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "TblMessage");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblRolePermissionRel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblRolePermissionRel",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblRolePermissionRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblRolePermissionRel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblRolePermissionRel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblRolePermissionRel",
                type: "datetime",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_CreatedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_DeleteById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropForeignKey(
                name: "FK_TblRolePermissionRel_TblUsers_ModifiedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropIndex(
                name: "IX_TblRolePermissionRel_CreatedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropIndex(
                name: "IX_TblRolePermissionRel_DeleteById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropIndex(
                name: "IX_TblRolePermissionRel_ModifiedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblRolePermissionRel");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblRolePermissionRel");

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "TblMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

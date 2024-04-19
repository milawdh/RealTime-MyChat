using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class TblPermissionSettedNotAudited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUser_CreatedById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUser_DeleteById",
                table: "TblPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TblPermission_TblUser_ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_CreatedById",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_DeleteById",
                table: "TblPermission");

            migrationBuilder.DropIndex(
                name: "IX_TblPermission_ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TblPermission");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TblPermission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TblPermission",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteById",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "TblPermission",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "TblPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TblPermission",
                type: "datetime",
                nullable: true);

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
        }
    }
}

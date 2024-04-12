using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "TblUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("62af09e2-6af8-ee11-b6e5-44af284397a1"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TblMessage",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TblMedia",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "FileMimType",
                table: "TblMedia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "TblMedia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FileServerId",
                table: "TblMedia",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "TblChatRoom",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newsequentialid())");

            migrationBuilder.CreateTable(
                name: "TblFileServer",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_TblFileServer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TblFileServer_TblUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblFileServer_TblUser_DeleteById",
                        column: x => x.DeleteById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TblFileServer_TblUser_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "TblUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblMedia_FileServerId",
                table: "TblMedia",
                column: "FileServerId");

            migrationBuilder.CreateIndex(
                name: "IX_TblFileServer_CreatedById",
                table: "TblFileServer",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TblFileServer_DeleteById",
                table: "TblFileServer",
                column: "DeleteById");

            migrationBuilder.CreateIndex(
                name: "IX_TblFileServer_ModifiedById",
                table: "TblFileServer",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TTblFileServer_Title",
                table: "TblFileServer",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TBlMedia_TblFileServer",
                table: "TblMedia",
                column: "FileServerId",
                principalTable: "TblFileServer",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBlMedia_TblFileServer",
                table: "TblMedia");

            migrationBuilder.DropTable(
                name: "TblFileServer");

            migrationBuilder.DropIndex(
                name: "IX_TblMedia_FileServerId",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "FileMimType",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "FileServerId",
                table: "TblMedia");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "TblUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("0c621b69-cebb-ee11-b6e1-44af2843979e"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("62af09e2-6af8-ee11-b6e5-44af284397a1"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TblMessage",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TblMedia",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "TblChatRoom",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newsequentialid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}

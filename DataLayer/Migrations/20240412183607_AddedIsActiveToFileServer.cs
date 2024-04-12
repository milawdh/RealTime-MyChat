using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveToFileServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBlMedia_TblFileServer",
                table: "TblMedia");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TblFileServer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TBlMedia_TblFileServer",
                table: "TblMedia",
                column: "FileServerId",
                principalTable: "TblFileServer",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBlMedia_TblFileServer",
                table: "TblMedia");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TblFileServer");

            migrationBuilder.AddForeignKey(
                name: "FK_TBlMedia_TblFileServer",
                table: "TblMedia",
                column: "FileServerId",
                principalTable: "TblFileServer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableNewsFile_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_News_NewsId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_NewsId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "File");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "News",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_FileId",
                table: "News",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_News_File_FileId",
                table: "News",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_File_FileId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_FileId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "News");

            migrationBuilder.AddColumn<long>(
                name: "NewsId",
                table: "File",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_NewsId",
                table: "File",
                column: "NewsId",
                unique: true,
                filter: "[NewsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_File_News_NewsId",
                table: "File",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id");
        }
    }
}

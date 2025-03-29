using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelationshipFileNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileIds",
                table: "News");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "News",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_News_FileId",
                table: "News",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_News_File_FileId",
                table: "News",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AddColumn<string>(
                name: "FileIds",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

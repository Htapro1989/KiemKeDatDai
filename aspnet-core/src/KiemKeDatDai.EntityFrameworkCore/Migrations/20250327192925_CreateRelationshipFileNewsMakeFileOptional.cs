using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelationshipFileNewsMakeFileOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_File_FileId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_FileId",
                table: "News");

            migrationBuilder.AlterColumn<long>(
                name: "FileId",
                table: "News",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.AlterColumn<long>(
                name: "FileId",
                table: "News",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}

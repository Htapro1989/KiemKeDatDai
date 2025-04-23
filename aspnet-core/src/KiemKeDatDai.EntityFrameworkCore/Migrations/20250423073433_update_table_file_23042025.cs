using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_file_23042025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountImportedTable",
                table: "File",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImportedStatus",
                table: "File",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Year",
                table: "DM_BieuMau",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountImportedTable",
                table: "File");

            migrationBuilder.DropColumn(
                name: "ImportedStatus",
                table: "File");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "DM_BieuMau");
        }
    }
}

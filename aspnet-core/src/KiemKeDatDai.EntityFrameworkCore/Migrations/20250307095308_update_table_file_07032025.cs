using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_file_07032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DVHCId",
                table: "File",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaDVHC",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DVHCId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "MaDVHC",
                table: "File");
        }
    }
}

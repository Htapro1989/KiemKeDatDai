using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_donvihanhchinh_10032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ma",
                table: "DonViHanhChinh",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ma",
                table: "DonViHanhChinh");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_db_09032025_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chiTieuId",
                table: "Data_District",
                newName: "ChiTieuId");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "Data_Commune",
                newName: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChiTieuId",
                table: "Data_District",
                newName: "chiTieuId");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Data_Commune",
                newName: "year");
        }
    }
}

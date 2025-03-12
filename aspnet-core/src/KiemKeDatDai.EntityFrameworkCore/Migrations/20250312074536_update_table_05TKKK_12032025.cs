using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_05TKKK_12032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KUA",
                table: "Bieu05TKKK_Xa",
                newName: "LUA");

            migrationBuilder.RenameColumn(
                name: "KUA",
                table: "Bieu05TKKK_Vung",
                newName: "LUA");

            migrationBuilder.RenameColumn(
                name: "KUA",
                table: "Bieu05TKKK_Tinh",
                newName: "LUA");

            migrationBuilder.RenameColumn(
                name: "KUA",
                table: "Bieu05TKKK_Huyen",
                newName: "LUA");

            migrationBuilder.RenameColumn(
                name: "KUA",
                table: "Bieu05TKKK",
                newName: "LUA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LUA",
                table: "Bieu05TKKK_Xa",
                newName: "KUA");

            migrationBuilder.RenameColumn(
                name: "LUA",
                table: "Bieu05TKKK_Vung",
                newName: "KUA");

            migrationBuilder.RenameColumn(
                name: "LUA",
                table: "Bieu05TKKK_Tinh",
                newName: "KUA");

            migrationBuilder.RenameColumn(
                name: "LUA",
                table: "Bieu05TKKK_Huyen",
                newName: "KUA");

            migrationBuilder.RenameColumn(
                name: "LUA",
                table: "Bieu05TKKK",
                newName: "KUA");
        }
    }
}

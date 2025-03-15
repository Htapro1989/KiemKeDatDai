using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_Bieu06TKKKQPAN_15032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaTinh",
                table: "Bieu06TKKKQPAN",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TinhId",
                table: "Bieu06TKKKQPAN",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaTinh",
                table: "Bieu06TKKKQPAN");

            migrationBuilder.DropColumn(
                name: "TinhId",
                table: "Bieu06TKKKQPAN");
        }
    }
}

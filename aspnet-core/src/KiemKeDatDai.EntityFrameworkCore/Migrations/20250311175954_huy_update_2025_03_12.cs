using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class huy_update_2025_03_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Xa",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Vung",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Tinh",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK_Huyen",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CoQuanNhaNuoc_TCN",
                table: "Bieu04TKKK",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

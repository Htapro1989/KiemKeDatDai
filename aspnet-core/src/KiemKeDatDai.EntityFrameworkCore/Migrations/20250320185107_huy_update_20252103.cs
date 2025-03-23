using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class huy_update_20252103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DienTichMucDich",
                table: "Data_Commune",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MdSDLuaChuyenDoi",
                table: "Data_Commune",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MdSDSoLuongDoiTuong",
                table: "Data_Commune",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MucDichSuDungKyTruoc",
                table: "Data_Commune",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DienTichMucDich",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "MdSDLuaChuyenDoi",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "MdSDSoLuongDoiTuong",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "MucDichSuDungKyTruoc",
                table: "Data_Commune");
        }
    }
}

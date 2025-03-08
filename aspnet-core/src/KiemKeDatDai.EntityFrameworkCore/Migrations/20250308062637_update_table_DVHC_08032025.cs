using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_DVHC_08032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiDuyet",
                table: "DonViHanhChinh",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "MaVung",
                table: "DonViHanhChinh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "DonViHanhChinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGui",
                table: "DonViHanhChinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SoDVHCCon",
                table: "DonViHanhChinh",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoDVHCDaDuyet",
                table: "DonViHanhChinh",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenVung",
                table: "DonViHanhChinh",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaVung",
                table: "DonViHanhChinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "DonViHanhChinh");

            migrationBuilder.DropColumn(
                name: "NgayGui",
                table: "DonViHanhChinh");

            migrationBuilder.DropColumn(
                name: "SoDVHCCon",
                table: "DonViHanhChinh");

            migrationBuilder.DropColumn(
                name: "SoDVHCDaDuyet",
                table: "DonViHanhChinh");

            migrationBuilder.DropColumn(
                name: "TenVung",
                table: "DonViHanhChinh");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiDuyet",
                table: "DonViHanhChinh",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

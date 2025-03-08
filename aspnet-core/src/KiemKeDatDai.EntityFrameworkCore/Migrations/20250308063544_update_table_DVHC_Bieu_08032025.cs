using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_table_DVHC_Bieu_08032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Xa");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Xa");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu02KKSL");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu02KKSL");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "NgayGui",
                table: "Bieu01TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "NgayGui",
                table: "Bieu01TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayGui",
                table: "Bieu01TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayGui",
                table: "Bieu01TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01TKKK");

            migrationBuilder.DropColumn(
                name: "NgayGui",
                table: "Bieu01TKKK");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01TKKK");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Xa");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Xa");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01KKSL");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01KKSL");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Xa");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Xa");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Xa");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Xa");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Xa");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Xa");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Tinh");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Huyen");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT");

            migrationBuilder.DropColumn(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayGui",
                table: "DonViHanhChinh",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDuyet",
                table: "DonViHanhChinh",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "VungId",
                table: "Bieu02KKSL_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaVung",
                table: "Bieu01KKSL_Vung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VungId",
                table: "Bieu01KKSL_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VungId",
                table: "Bieu01cKKNLT_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VungId",
                table: "Bieu01bKKNLT_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VungId",
                table: "Bieu01aKKNLT_Vung",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VungId",
                table: "Bieu02KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "MaVung",
                table: "Bieu01KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "VungId",
                table: "Bieu01KKSL_Vung");

            migrationBuilder.DropColumn(
                name: "VungId",
                table: "Bieu01cKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "VungId",
                table: "Bieu01bKKNLT_Vung");

            migrationBuilder.DropColumn(
                name: "VungId",
                table: "Bieu01aKKNLT_Vung");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayGui",
                table: "DonViHanhChinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayDuyet",
                table: "DonViHanhChinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu02KKSL_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu02KKSL_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu02KKSL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu02KKSL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGui",
                table: "Bieu01TKKK_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGui",
                table: "Bieu01TKKK_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGui",
                table: "Bieu01TKKK_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01TKKK_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGui",
                table: "Bieu01TKKK_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01TKKK_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01TKKK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGui",
                table: "Bieu01TKKK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01TKKK",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01KKSL_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01KKSL_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01KKSL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01KKSL",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01cKKNLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01cKKNLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01bKKNLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01bKKNLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Xa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Vung",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Tinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT_Huyen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "Bieu01aKKNLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayLapBieu",
                table: "Bieu01aKKNLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

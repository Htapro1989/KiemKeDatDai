using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class huy_update_20250316_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu05TKKK_Xa",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu05TKKK_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu05TKKK_Tinh",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu05TKKK_Huyen",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu05TKKK",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu04TKKK_Xa",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu04TKKK_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu04TKKK_Tinh",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu04TKKK_Huyen",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu04TKKK",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu03TKKK_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu03TKKK_Tinh",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu03TKKK_Huyen",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu03TKKK",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu02TKKK_Xa",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu02TKKK_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu02TKKK_Tinh",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu02TKKK_Huyen",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu02TKKK",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu01TKKK_Xa",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu01TKKK_Vung",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu01TKKK_Tinh",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu01TKKK_Huyen",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sequence",
                table: "Bieu01TKKK",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu05TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu05TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu05TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu05TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu05TKKK");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu04TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu04TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu04TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu04TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu04TKKK");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu03TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu03TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu03TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu03TKKK");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu02TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu02TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu02TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu02TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu02TKKK");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu01TKKK_Xa");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu01TKKK_Vung");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu01TKKK_Tinh");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu01TKKK_Huyen");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "Bieu01TKKK");
        }
    }
}

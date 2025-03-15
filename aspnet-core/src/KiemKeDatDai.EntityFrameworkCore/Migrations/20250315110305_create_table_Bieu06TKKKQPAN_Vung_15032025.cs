using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class create_table_Bieu06TKKKQPAN_Vung_15032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bieu06TKKKQPAN_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichDatQuocPhong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichKetHopKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoaiDatKetHopKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDaDoDac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoGCNDaCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDaCapGCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhId = table.Column<long>(type: "bigint", nullable: true),
                    MaVung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VungId = table.Column<long>(type: "bigint", nullable: true),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bieu06TKKKQPAN_Vung", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bieu06TKKKQPAN_Vung");
        }
    }
}

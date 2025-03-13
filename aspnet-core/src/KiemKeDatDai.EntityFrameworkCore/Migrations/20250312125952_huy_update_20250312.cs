using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class huy_update_20250312 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Data_BienDong",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThuaTruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThuaSauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChuSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiThuaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichBienDong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MDSDTruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MDSDSauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DTTruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DTSauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHKDTruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHKDSauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NDThayDoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Data_BienDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoanhDat_KyTruoc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaLoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDatId = table.Column<long>(type: "bigint", nullable: false),
                    MaLoaiDatKT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    DTKhongGian = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_KhoanhDat_KyTruoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoLieuKyTruoc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDatId = table.Column<long>(type: "bigint", nullable: false),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_SoLieuKyTruoc", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data_BienDong");

            migrationBuilder.DropTable(
                name: "KhoanhDat_KyTruoc");

            migrationBuilder.DropTable(
                name: "SoLieuKyTruoc");
        }
    }
}

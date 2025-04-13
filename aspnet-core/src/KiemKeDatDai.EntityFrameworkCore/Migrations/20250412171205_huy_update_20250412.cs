using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class huy_update_20250412 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bieu02aKKNLT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichTheoQDGiaoThue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichGiaoDat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichChoThueDat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichChuaXacDinhGiaoThue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL1000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL2000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL5000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL10000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SoGCNDaCap = table.Column<long>(type: "bigint", nullable: false),
                    DienTichGCNDaCap = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDaBanGiao = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhId = table.Column<long>(type: "bigint", nullable: true),
                    sequence = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02aKKNLT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02aKKNLT_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichTheoQDGiaoThue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichGiaoDat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichChoThueDat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichChuaXacDinhGiaoThue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL1000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL2000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL5000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL10000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SoGCNDaCap = table.Column<long>(type: "bigint", nullable: false),
                    DienTichGCNDaCap = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDaBanGiao = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhId = table.Column<long>(type: "bigint", nullable: true),
                    sequence = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02aKKNLT_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02aKKNLT_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichTheoQDGiaoThue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichGiaoDat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichChoThueDat = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichChuaXacDinhGiaoThue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL1000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL2000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL5000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDoDacTL10000 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SoGCNDaCap = table.Column<long>(type: "bigint", nullable: false),
                    DienTichGCNDaCap = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DienTichDaBanGiao = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhId = table.Column<long>(type: "bigint", nullable: true),
                    MaVung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VungId = table.Column<long>(type: "bigint", nullable: true),
                    sequence = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02aKKNLT_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Data_TangGiamKhac",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TangGiamKhacId = table.Column<long>(type: "bigint", nullable: false),
                    MaDVHCCapXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichTangGiamKhac = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
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
                    table.PrimaryKey("PK_Data_TangGiamKhac", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bieu02aKKNLT");

            migrationBuilder.DropTable(
                name: "Bieu02aKKNLT_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu02aKKNLT_Vung");

            migrationBuilder.DropTable(
                name: "Data_TangGiamKhac");
        }
    }
}

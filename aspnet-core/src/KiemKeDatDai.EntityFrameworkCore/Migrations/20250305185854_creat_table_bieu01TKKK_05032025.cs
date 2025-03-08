using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class creat_table_bieu01TKKK_05032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bieu01TKKK",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichDVHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_NGV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongDuocGiaoQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01TKKK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01TKKK_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichDVHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_NGV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongDuocGiaoQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HuyenId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01TKKK_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01TKKK_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichDVHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_NGV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongDuocGiaoQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01TKKK_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01TKKK_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichDVHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_NGV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongDuocGiaoQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01TKKK_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01TKKK_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichDVHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_NGV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoTheoDoiTuongDuocGiaoQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XaId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01TKKK_Xa", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bieu01TKKK");

            migrationBuilder.DropTable(
                name: "Bieu01TKKK_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu01TKKK_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu01TKKK_Vung");

            migrationBuilder.DropTable(
                name: "Bieu01TKKK_Xa");
        }
    }
}

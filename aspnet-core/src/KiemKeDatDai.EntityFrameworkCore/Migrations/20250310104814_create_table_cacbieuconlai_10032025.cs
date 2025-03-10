using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class create_table_cacbieuconlai_10032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bieu02KKSL");

            migrationBuilder.CreateTable(
                name: "Bieu02TKKK",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02TKKK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02TKKK_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02TKKK_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02TKKK_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02TKKK_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02TKKK_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02TKKK_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02TKKK_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    CoQuanNhaNuoc_TCQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02TKKK_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu03TKKK",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichTheoDVHC = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu03TKKK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu03TKKK_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichTheoDVHC = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu03TKKK_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu03TKKK_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichTheoDVHC = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu03TKKK_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu03TKKK_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichTheoDVHC = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu03TKKK_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu04TKKK",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSo_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu04TKKK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu04TKKK_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSo_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu04TKKK_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu04TKKK_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSo_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu04TKKK_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu04TKKK_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSo_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu04TKKK_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu04TKKK_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSo_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSo_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaNhanTrongNuoc_CNV_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucXaHoi_TXH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_TKT_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKhac_TKH_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucTonGiao_TTG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDS_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucNuocNgoai_TNG_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiGocVietNamONuocNgoai_CNN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTeVonNuocNgoai_TVN_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoQuanNhaNuoc_TCQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonViSuNghiep_TSQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToChucKinhTe_KTQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_DT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CongDongDanCu_CDQ_CC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu04TKKK_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu05TKKK",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KUA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LMU = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ONT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ODT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TSC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CQP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CAN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DVH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DYT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TMD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DPC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DRA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DBV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MNC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu05TKKK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu05TKKK_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KUA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LMU = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ONT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ODT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TSC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CQP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CAN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DVH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DYT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TMD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DPC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DRA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DBV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MNC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu05TKKK_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu05TKKK_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KUA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LMU = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ONT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ODT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TSC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CQP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CAN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DVH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DYT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TMD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DPC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DRA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DBV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MNC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu05TKKK_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu05TKKK_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KUA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LMU = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ONT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ODT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TSC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CQP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CAN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DVH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DYT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TMD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DPC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DRA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DBV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MNC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu05TKKK_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu05TKKK_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KUA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RPH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LMU = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ONT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ODT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TSC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CQP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CAN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DVH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DXH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DYT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DMT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TMD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DTL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DPC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DRA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DNL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DBV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DKV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TIN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NTD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MNC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SON = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PNK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Bieu05TKKK_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu06TKKKQPAN",
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
                    table.PrimaryKey("PK_Bieu06TKKKQPAN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu06TKKKQPAN_Tinh",
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
                    table.PrimaryKey("PK_Bieu06TKKKQPAN_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BieuPhuLucIII",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HienTrang_MLD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KyTruoc_MLD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiDatSDKetHop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HienTrang_MDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KyTruoc_MDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKhuVucTongHop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BieuPhuLucIII", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BieuPhuLucIV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoHieuThua_TruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoHieuThua_SauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNguoiSDDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiKhoanhDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTichCoBienDong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaLoaiDat_TruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiDat_SauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiDoiTuong_TruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiDoiTuong_SauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TTKhoanhDat_TruocBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TTKhoanhDat_SauBienDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDungThayDoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BieuPhuLucIV", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bieu02TKKK");

            migrationBuilder.DropTable(
                name: "Bieu02TKKK_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu02TKKK_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu02TKKK_Vung");

            migrationBuilder.DropTable(
                name: "Bieu02TKKK_Xa");

            migrationBuilder.DropTable(
                name: "Bieu03TKKK");

            migrationBuilder.DropTable(
                name: "Bieu03TKKK_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu03TKKK_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu03TKKK_Vung");

            migrationBuilder.DropTable(
                name: "Bieu04TKKK");

            migrationBuilder.DropTable(
                name: "Bieu04TKKK_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu04TKKK_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu04TKKK_Vung");

            migrationBuilder.DropTable(
                name: "Bieu04TKKK_Xa");

            migrationBuilder.DropTable(
                name: "Bieu05TKKK");

            migrationBuilder.DropTable(
                name: "Bieu05TKKK_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu05TKKK_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu05TKKK_Vung");

            migrationBuilder.DropTable(
                name: "Bieu05TKKK_Xa");

            migrationBuilder.DropTable(
                name: "Bieu06TKKKQPAN");

            migrationBuilder.DropTable(
                name: "Bieu06TKKKQPAN_Tinh");

            migrationBuilder.DropTable(
                name: "BieuPhuLucIII");

            migrationBuilder.DropTable(
                name: "BieuPhuLucIV");

            migrationBuilder.CreateTable(
                name: "Bieu02KKSL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    NamSatLo = table.Column<int>(type: "int", nullable: false),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bieu02KKSL", x => x.Id);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class create_table_kythongkekiemke_07032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bieu01aKKNLT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongLua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayHangNamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayLauNam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungDacDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungPhongHo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungSanXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNuoiTrongThuySan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSXKDPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatCongCong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNghiaTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatMatNuocChuyenDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatPhiNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDatChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01aKKNLT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01aKKNLT_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongLua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayHangNamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayLauNam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungDacDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungPhongHo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungSanXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNuoiTrongThuySan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSXKDPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatCongCong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNghiaTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatMatNuocChuyenDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatPhiNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDatChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01aKKNLT_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01aKKNLT_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongLua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayHangNamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayLauNam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungDacDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungPhongHo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungSanXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNuoiTrongThuySan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSXKDPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatCongCong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNghiaTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatMatNuocChuyenDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatPhiNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDatChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01aKKNLT_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01aKKNLT_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongLua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayHangNamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayLauNam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungDacDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungPhongHo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungSanXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNuoiTrongThuySan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSXKDPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatCongCong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNghiaTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatMatNuocChuyenDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatPhiNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDatChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaVung = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01aKKNLT_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01aKKNLT_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongLua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayHangNamKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTrongCayLauNam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungDacDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungPhongHo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatRungSanXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNuoiTrongThuySan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongSoDatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSXKDPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatCongCong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNghiaTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatMatNuocChuyenDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CacLoaiDatPhiNongNghiepKhac = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTichDatChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01aKKNLT_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01bKKNLT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatBiLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyNhungChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01bKKNLT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01bKKNLT_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatBiLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyNhungChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01bKKNLT_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01bKKNLT_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatBiLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyNhungChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01bKKNLT_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01bKKNLT_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatBiLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyNhungChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaVung = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01bKKNLT_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01bKKNLT_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDVSDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatPhiNongNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatBiLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyNhungChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01bKKNLT_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01cKKNLT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TomngDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01cKKNLT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01cKKNLT_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TomngDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HuyenId = table.Column<long>(type: "bigint", nullable: true),
                    MaHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01cKKNLT_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01cKKNLT_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TomngDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01cKKNLT_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01cKKNLT_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TomngDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaVung = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu01cKKNLT_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01cKKNLT_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichQuanLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatSuDungKhongDungMucDich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TomngDienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoKhoanTrang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatDangGiaoChoMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLienDoanh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatLanChiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatTranhChap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatGiaoQuanLyChuaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01cKKNLT_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01KKSL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSatLo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungDoiNui = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichBoiDap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01KKSL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01KKSL_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSatLo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungDoiNui = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichBoiDap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HuyenId = table.Column<long>(type: "bigint", nullable: true),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01KKSL_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01KKSL_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSatLo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungDoiNui = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichBoiDap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhId = table.Column<long>(type: "bigint", nullable: true),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01KKSL_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01KKSL_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSatLo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungDoiNui = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichBoiDap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01KKSL_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu01KKSL_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongDienTichSatLo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungDoiNui = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatLoVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongDienTichBoiDap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoSong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoiDapVungBoBien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XaId = table.Column<long>(type: "bigint", nullable: true),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu01KKSL_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02KKSL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamSatLo = table.Column<int>(type: "int", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02KKSL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02KKSL_Huyen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamSatLo = table.Column<int>(type: "int", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02KKSL_Huyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02KKSL_Tinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamSatLo = table.Column<int>(type: "int", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02KKSL_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02KKSL_Vung",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamSatLo = table.Column<int>(type: "int", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaVung = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Bieu02KKSL_Vung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieu02KKSL_Xa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamSatLo = table.Column<int>(type: "int", nullable: false),
                    NgayLapBieu = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Bieu02KKSL_Xa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KyThongKeKiemKe",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_KyThongKeKiemKe", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bieu01aKKNLT");

            migrationBuilder.DropTable(
                name: "Bieu01aKKNLT_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu01aKKNLT_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu01aKKNLT_Vung");

            migrationBuilder.DropTable(
                name: "Bieu01aKKNLT_Xa");

            migrationBuilder.DropTable(
                name: "Bieu01bKKNLT");

            migrationBuilder.DropTable(
                name: "Bieu01bKKNLT_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu01bKKNLT_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu01bKKNLT_Vung");

            migrationBuilder.DropTable(
                name: "Bieu01bKKNLT_Xa");

            migrationBuilder.DropTable(
                name: "Bieu01cKKNLT");

            migrationBuilder.DropTable(
                name: "Bieu01cKKNLT_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu01cKKNLT_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu01cKKNLT_Vung");

            migrationBuilder.DropTable(
                name: "Bieu01cKKNLT_Xa");

            migrationBuilder.DropTable(
                name: "Bieu01KKSL");

            migrationBuilder.DropTable(
                name: "Bieu01KKSL_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu01KKSL_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu01KKSL_Vung");

            migrationBuilder.DropTable(
                name: "Bieu01KKSL_Xa");

            migrationBuilder.DropTable(
                name: "Bieu02KKSL");

            migrationBuilder.DropTable(
                name: "Bieu02KKSL_Huyen");

            migrationBuilder.DropTable(
                name: "Bieu02KKSL_Tinh");

            migrationBuilder.DropTable(
                name: "Bieu02KKSL_Vung");

            migrationBuilder.DropTable(
                name: "Bieu02KKSL_Xa");

            migrationBuilder.DropTable(
                name: "KyThongKeKiemKe");
        }
    }
}

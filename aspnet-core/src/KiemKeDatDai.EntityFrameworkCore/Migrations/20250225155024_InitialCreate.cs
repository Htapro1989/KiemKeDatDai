using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_dvhc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    level = table.Column<long>(type: "bigint", nullable: false),
                    activate = table.Column<bool>(type: "bit", nullable: false),
                    year_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_admin_dvhc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_role_group",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_admin_role_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_setup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expired_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    server_file_upload = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_admin_setup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_token",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expired_date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_admin_token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "category_place",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    place_id = table.Column<long>(type: "bigint", nullable: false),
                    activate = table.Column<bool>(type: "bit", nullable: false),
                    year_id = table.Column<long>(type: "bigint", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_category_place", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "category_role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_category_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_commune",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoanhDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDVHCCapXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoThuTuKhoanhDat = table.Column<long>(type: "bigint", nullable: false),
                    DTKhongGian = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaDoiTuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDoiTuongKyTruoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDungNLT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSatLoBoiDap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSanGon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSanBay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chiTieuId = table.Column<long>(type: "bigint", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    year_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_data_commune", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_district",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoanhDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDVHCCapXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoThuTuKhoanhDat = table.Column<long>(type: "bigint", nullable: false),
                    DTKhongGian = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaDoiTuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDoiTuongKyTruoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDungNLT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSatLoBoiDap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSanGon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSanBay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chiTieuId = table.Column<long>(type: "bigint", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    year_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_data_district", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_province",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoanhDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDVHCCapXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDVHCCapHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDVHCCapTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoThuTuKhoanhDat = table.Column<long>(type: "bigint", nullable: false),
                    DTKhongGian = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaDoiTuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDoiTuongKyTruoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDungNLT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSatLoBoiDap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSanGon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MdSDSanBay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chiTieuId = table.Column<long>(type: "bigint", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    year_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_data_province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_target",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDVHCCapXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKhoanhDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDichSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDoiTuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<long>(type: "bigint", nullable: false),
                    LoaiChiTieu = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_data_target", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_dvhc");

            migrationBuilder.DropTable(
                name: "admin_role_group");

            migrationBuilder.DropTable(
                name: "admin_setup");

            migrationBuilder.DropTable(
                name: "admin_token");

            migrationBuilder.DropTable(
                name: "category_place");

            migrationBuilder.DropTable(
                name: "category_role");

            migrationBuilder.DropTable(
                name: "data_commune");

            migrationBuilder.DropTable(
                name: "data_district");

            migrationBuilder.DropTable(
                name: "data_province");

            migrationBuilder.DropTable(
                name: "data_target");
        }
    }
}

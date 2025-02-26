using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class update_db_27022025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_target",
                table: "data_target");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_province",
                table: "data_province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_district",
                table: "data_district");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_commune",
                table: "data_commune");

            migrationBuilder.DropColumn(
                name: "MaDVHCCapHuyen",
                table: "data_province");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "data_province");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "data_district");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "data_commune");

            migrationBuilder.RenameTable(
                name: "data_target",
                newName: "Data_Target");

            migrationBuilder.RenameTable(
                name: "data_province",
                newName: "Data_Province");

            migrationBuilder.RenameTable(
                name: "data_district",
                newName: "Data_District");

            migrationBuilder.RenameTable(
                name: "data_commune",
                newName: "Data_Commune");

            migrationBuilder.RenameColumn(
                name: "year_id",
                table: "Data_Province",
                newName: "year_code");

            migrationBuilder.RenameColumn(
                name: "MaDVHCCapXa",
                table: "Data_Province",
                newName: "MaVung");

            migrationBuilder.RenameColumn(
                name: "MaDVHCCapTinh",
                table: "Data_Province",
                newName: "MaTinh");

            migrationBuilder.RenameColumn(
                name: "year_id",
                table: "Data_District",
                newName: "year_code");

            migrationBuilder.RenameColumn(
                name: "MaDVHCCapXa",
                table: "Data_District",
                newName: "MaVung");

            migrationBuilder.RenameColumn(
                name: "year_id",
                table: "Data_Commune",
                newName: "year_code");

            migrationBuilder.RenameColumn(
                name: "MaDVHCCapXa",
                table: "Data_Commune",
                newName: "MaXa");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Data_Province",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaHuyen",
                table: "Data_District",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTinh",
                table: "Data_District",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Data_District",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TinhId",
                table: "Data_District",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HuyenId",
                table: "Data_Commune",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MaHuyen",
                table: "Data_Commune",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTinh",
                table: "Data_Commune",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaVung",
                table: "Data_Commune",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Data_Commune",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TinhId",
                table: "Data_Commune",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DonViHanhChinhId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data_Target",
                table: "Data_Target",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data_Province",
                table: "Data_Province",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data_District",
                table: "Data_District",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data_Commune",
                table: "Data_Commune",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConfigSystem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expired_auth = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ConfigSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonViHanhChinh",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parent_id = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Year_Id = table.Column<long>(type: "bigint", nullable: false),
                    TrangThaiDuyet = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DonViHanhChinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_File", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigSystem");

            migrationBuilder.DropTable(
                name: "DonViHanhChinh");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Data_Target",
                table: "Data_Target");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Data_Province",
                table: "Data_Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Data_District",
                table: "Data_District");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Data_Commune",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Data_Province");

            migrationBuilder.DropColumn(
                name: "MaHuyen",
                table: "Data_District");

            migrationBuilder.DropColumn(
                name: "MaTinh",
                table: "Data_District");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Data_District");

            migrationBuilder.DropColumn(
                name: "TinhId",
                table: "Data_District");

            migrationBuilder.DropColumn(
                name: "HuyenId",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "MaHuyen",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "MaTinh",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "MaVung",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "TinhId",
                table: "Data_Commune");

            migrationBuilder.DropColumn(
                name: "DonViHanhChinhId",
                table: "AbpUsers");

            migrationBuilder.RenameTable(
                name: "Data_Target",
                newName: "data_target");

            migrationBuilder.RenameTable(
                name: "Data_Province",
                newName: "data_province");

            migrationBuilder.RenameTable(
                name: "Data_District",
                newName: "data_district");

            migrationBuilder.RenameTable(
                name: "Data_Commune",
                newName: "data_commune");

            migrationBuilder.RenameColumn(
                name: "year_code",
                table: "data_province",
                newName: "year_id");

            migrationBuilder.RenameColumn(
                name: "MaVung",
                table: "data_province",
                newName: "MaDVHCCapXa");

            migrationBuilder.RenameColumn(
                name: "MaTinh",
                table: "data_province",
                newName: "MaDVHCCapTinh");

            migrationBuilder.RenameColumn(
                name: "year_code",
                table: "data_district",
                newName: "year_id");

            migrationBuilder.RenameColumn(
                name: "MaVung",
                table: "data_district",
                newName: "MaDVHCCapXa");

            migrationBuilder.RenameColumn(
                name: "year_code",
                table: "data_commune",
                newName: "year_id");

            migrationBuilder.RenameColumn(
                name: "MaXa",
                table: "data_commune",
                newName: "MaDVHCCapXa");

            migrationBuilder.AddColumn<string>(
                name: "MaDVHCCapHuyen",
                table: "data_province",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "data_province",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "data_district",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "data_commune",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_target",
                table: "data_target",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_province",
                table: "data_province",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_district",
                table: "data_district",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_commune",
                table: "data_commune",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "admin_dvhc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    activate = table.Column<bool>(type: "bit", nullable: false),
                    level = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    year_id = table.Column<long>(type: "bigint", nullable: false)
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
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
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
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    expired_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    server_file_upload = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expired_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    token = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
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
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    activate = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    place_id = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    year_id = table.Column<long>(type: "bigint", nullable: false)
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
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_role", x => x.Id);
                });
        }
    }
}

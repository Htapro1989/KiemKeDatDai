using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemKeDatDai.Migrations
{
    /// <inheritdoc />
    public partial class huy_update_20250316_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_DT",
                table: "Bieu04TKKK_Xa",
                newName: "NguoiGocVietNamONuocNgoai_NGV_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_CC",
                table: "Bieu04TKKK_Xa",
                newName: "NguoiGocVietNamONuocNgoai_NGV_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_DT",
                table: "Bieu04TKKK_Vung",
                newName: "NguoiGocVietNamONuocNgoai_NGV_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_CC",
                table: "Bieu04TKKK_Vung",
                newName: "NguoiGocVietNamONuocNgoai_NGV_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_DT",
                table: "Bieu04TKKK_Tinh",
                newName: "NguoiGocVietNamONuocNgoai_NGV_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_CC",
                table: "Bieu04TKKK_Tinh",
                newName: "NguoiGocVietNamONuocNgoai_NGV_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_DT",
                table: "Bieu04TKKK_Huyen",
                newName: "NguoiGocVietNamONuocNgoai_NGV_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_CC",
                table: "Bieu04TKKK_Huyen",
                newName: "NguoiGocVietNamONuocNgoai_NGV_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_DT",
                table: "Bieu04TKKK",
                newName: "NguoiGocVietNamONuocNgoai_NGV_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_CNN_CC",
                table: "Bieu04TKKK",
                newName: "NguoiGocVietNamONuocNgoai_NGV_CC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_DT",
                table: "Bieu04TKKK_Xa",
                newName: "NguoiGocVietNamONuocNgoai_CNN_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_CC",
                table: "Bieu04TKKK_Xa",
                newName: "NguoiGocVietNamONuocNgoai_CNN_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_DT",
                table: "Bieu04TKKK_Vung",
                newName: "NguoiGocVietNamONuocNgoai_CNN_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_CC",
                table: "Bieu04TKKK_Vung",
                newName: "NguoiGocVietNamONuocNgoai_CNN_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_DT",
                table: "Bieu04TKKK_Tinh",
                newName: "NguoiGocVietNamONuocNgoai_CNN_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_CC",
                table: "Bieu04TKKK_Tinh",
                newName: "NguoiGocVietNamONuocNgoai_CNN_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_DT",
                table: "Bieu04TKKK_Huyen",
                newName: "NguoiGocVietNamONuocNgoai_CNN_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_CC",
                table: "Bieu04TKKK_Huyen",
                newName: "NguoiGocVietNamONuocNgoai_CNN_CC");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_DT",
                table: "Bieu04TKKK",
                newName: "NguoiGocVietNamONuocNgoai_CNN_DT");

            migrationBuilder.RenameColumn(
                name: "NguoiGocVietNamONuocNgoai_NGV_CC",
                table: "Bieu04TKKK",
                newName: "NguoiGocVietNamONuocNgoai_CNN_CC");
        }
    }
}

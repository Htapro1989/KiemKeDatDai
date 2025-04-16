using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu01TKKK_Xa")]
    public class Bieu01TKKK_Xa : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? LoaiDat { get; set; }
        public string? Ma { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTichDVHC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongSoTheoDoiTuongSuDung { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CaNhanTrongNuoc_CNV { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NguoiVietNamONuocNgoai_CNN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CoQuanNhaNuoc_TCN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DonViSuNghiep_TSN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucXaHoi_TXH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTe_TKT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKhac_TKH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucTonGiao_TTG { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CongDongDanCu_CDS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucNuocNgoai_TNG { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NguoiGocVietNamONuocNgoai_NGV { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTeVonNuocNgoai_TVN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongSoTheoDoiTuongDuocGiaoQuanLy { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CoQuanNhaNuoc_TCQ { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DonViSuNghiep_TSQ { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTe_KTQ { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CongDongDanCu_CDQ { get; set; }
        public string? MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

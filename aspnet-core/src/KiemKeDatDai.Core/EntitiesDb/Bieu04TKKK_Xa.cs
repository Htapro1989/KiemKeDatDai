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
    [Table("Bieu04TKKK_Xa")]
    public class Bieu04TKKK_Xa : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongSo_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongSo_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CaNhanTrongNuoc_CNV_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CaNhanTrongNuoc_CNV_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NguoiVietNamONuocNgoai_CNN_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NguoiVietNamONuocNgoai_CNN_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CoQuanNhaNuoc_TCN_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CoQuanNhaNuoc_TCN_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DonViSuNghiep_TSN_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DonViSuNghiep_TSN_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucXaHoi_TXH_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucXaHoi_TXH_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTe_TKT_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTe_TKT_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKhac_TKH_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKhac_TKH_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucTonGiao_TTG_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucTonGiao_TTG_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CongDongDanCu_CDS_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CongDongDanCu_CDS_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucNuocNgoai_TNG_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucNuocNgoai_TNG_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NguoiGocVietNamONuocNgoai_NGV_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NguoiGocVietNamONuocNgoai_NGV_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTeVonNuocNgoai_TVN_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTeVonNuocNgoai_TVN_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CoQuanNhaNuoc_TCQ_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CoQuanNhaNuoc_TCQ_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DonViSuNghiep_TSQ_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DonViSuNghiep_TSQ_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTe_KTQ_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ToChucKinhTe_KTQ_CC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CongDongDanCu_CDQ_DT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CongDongDanCu_CDQ_CC { get; set; }
        public string MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

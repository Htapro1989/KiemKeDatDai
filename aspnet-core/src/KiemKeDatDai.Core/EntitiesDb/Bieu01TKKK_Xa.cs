using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu01TKKK_Xa")]
    public class Bieu01TKKK_Xa : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        public decimal TongDienTichDVHC { get; set; }
        public decimal TongSoTheoDoiTuongSuDung { get; set; }
        public decimal CaNhanTrongNuoc_CNV { get; set; }
        public decimal NguoiVietNamONuocNgoai_CNN { get; set; }
        public decimal CoQuanNhaNuoc_TCN { get; set; }
        public decimal DonViSuNghiep_TSN { get; set; }
        public decimal ToChucXaHoi_TXH { get; set; }
        public decimal ToChucKinhTe_TKT { get; set; }
        public decimal ToChucKhac_TKH { get; set; }
        public decimal ToChucTonGiao_TTG { get; set; }
        public decimal CongDongDanCu_CDS { get; set; }
        public decimal ToChucNuocNgoai_TNG { get; set; }
        public decimal NguoiGocVietNamONuocNgoai_NGV { get; set; }
        public decimal ToChucKinhTeVonNuocNgoai_TVN { get; set; }
        public decimal TongSoTheoDoiTuongDuocGiaoQuanLy { get; set; }
        public decimal CoQuanNhaNuoc_TCQ { get; set; }
        public decimal DonViSuNghiep_TSQ { get; set; }
        public decimal ToChucKinhTe_KTQ { get; set; }
        public decimal CongDongDanCu_CDQ { get; set; }
        public string MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

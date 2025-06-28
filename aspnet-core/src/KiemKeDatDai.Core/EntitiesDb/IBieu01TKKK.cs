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

    public interface IBieu01TKKK
    {
        string? STT { get; set; }
        string? LoaiDat { get; set; }
        string? Ma { get; set; }
        decimal TongDienTichDVHC { get; set; }
        decimal TongSoTheoDoiTuongSuDung { get; set; }
        decimal CaNhanTrongNuoc_CNV { get; set; }
        decimal NguoiVietNamONuocNgoai_CNN { get; set; }
        decimal CoQuanNhaNuoc_TCN { get; set; }
        decimal DonViSuNghiep_TSN { get; set; }
        decimal ToChucXaHoi_TXH { get; set; }
        decimal ToChucKinhTe_TKT { get; set; }
        decimal ToChucKhac_TKH { get; set; }
        decimal ToChucTonGiao_TTG { get; set; }
        decimal CongDongDanCu_CDS { get; set; }
        decimal ToChucNuocNgoai_TNG { get; set; }
        decimal NguoiGocVietNamONuocNgoai_NGV { get; set; }
        decimal ToChucKinhTeVonNuocNgoai_TVN { get; set; }
        decimal TongSoTheoDoiTuongDuocGiaoQuanLy { get; set; }
        decimal CoQuanNhaNuoc_TCQ { get; set; }
        decimal DonViSuNghiep_TSQ { get; set; }
        decimal ToChucKinhTe_KTQ { get; set; }
        decimal CongDongDanCu_CDQ { get; set; }
        long Year { get; set; }
        bool? Active { get; set; }
        long? sequence { get; set; }
    }
}

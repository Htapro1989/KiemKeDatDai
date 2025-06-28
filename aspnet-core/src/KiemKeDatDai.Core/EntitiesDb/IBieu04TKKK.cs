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
    public interface IBieu04TKKK
    {
        string? STT { get; set; }
        string? LoaiDat { get; set; }
        string? Ma { get; set; }
        decimal TongSo_DT { get; set; }
        decimal TongSo_CC { get; set; }
        decimal CaNhanTrongNuoc_CNV_DT { get; set; }
        decimal CaNhanTrongNuoc_CNV_CC { get; set; }
        decimal NguoiVietNamONuocNgoai_CNN_DT { get; set; }
        decimal NguoiVietNamONuocNgoai_CNN_CC { get; set; }
        decimal CoQuanNhaNuoc_TCN_DT { get; set; }
        decimal CoQuanNhaNuoc_TCN_CC { get; set; }
        decimal DonViSuNghiep_TSN_DT { get; set; }
        decimal DonViSuNghiep_TSN_CC { get; set; }
        decimal ToChucXaHoi_TXH_DT { get; set; }
        decimal ToChucXaHoi_TXH_CC { get; set; }
        decimal ToChucKinhTe_TKT_DT { get; set; }
        decimal ToChucKinhTe_TKT_CC { get; set; }
        decimal ToChucKhac_TKH_DT { get; set; }
        decimal ToChucKhac_TKH_CC { get; set; }
        decimal ToChucTonGiao_TTG_DT { get; set; }
        decimal ToChucTonGiao_TTG_CC { get; set; }
        decimal CongDongDanCu_CDS_DT { get; set; }
        decimal CongDongDanCu_CDS_CC { get; set; }
        decimal ToChucNuocNgoai_TNG_DT { get; set; }
        decimal ToChucNuocNgoai_TNG_CC { get; set; }
        decimal NguoiGocVietNamONuocNgoai_NGV_DT { get; set; }
        decimal NguoiGocVietNamONuocNgoai_NGV_CC { get; set; }
        decimal ToChucKinhTeVonNuocNgoai_TVN_DT { get; set; }
        decimal ToChucKinhTeVonNuocNgoai_TVN_CC { get; set; }
        decimal CoQuanNhaNuoc_TCQ_DT { get; set; }
        decimal CoQuanNhaNuoc_TCQ_CC { get; set; }
        decimal DonViSuNghiep_TSQ_DT { get; set; }
        decimal DonViSuNghiep_TSQ_CC { get; set; }
        decimal ToChucKinhTe_KTQ_DT { get; set; }
        decimal ToChucKinhTe_KTQ_CC { get; set; }
        decimal CongDongDanCu_CDQ_DT { get; set; }
        decimal CongDongDanCu_CDQ_CC { get; set; }

        long Year { get; set; }
        bool? Active { get; set; }
        long? sequence { get; set; }
    }
}

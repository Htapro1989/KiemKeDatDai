﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu01bKKNLT_Xa")]
    public class Bieu01bKKNLT_Xa : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string TenDVSDD { get; set; }
        public decimal TongDienTichSuDung { get; set; }
        public decimal DatNongNghiep { get; set; }
        public decimal DatPhiNongNghiep { get; set; }
        public decimal DatSuDungKhongDungMucDich { get; set; }
        public decimal TongDienTich { get; set; }
        public decimal DatDangGiaoKhoanTrang { get; set; }
        public decimal DatDangGiaoChoMuon { get; set; }
        public decimal DatDangLienDoanh { get; set; }
        public decimal DatBiLanChiem { get; set; }
        public decimal DatTranhChap { get; set; }
        public decimal DatGiaoQuanLyNhungChuaSuDung{ get; set; }
        public string MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

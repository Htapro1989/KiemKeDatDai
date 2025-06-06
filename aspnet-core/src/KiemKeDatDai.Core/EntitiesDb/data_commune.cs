﻿using Abp.Domain.Entities.Auditing;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KiemKeDatDai.EntitiesDb
{
    [Table("Data_Commune")]
    public class Data_Commune : FullAuditedEntity<long>
    {
        public string? MaXa { get; set; }
        public string? MaHuyen { get; set; }
        public string? MaTinh { get; set; }
        public string? MaVung { get; set; }
        public long HuyenId { get; set; }
        public long TinhId { get; set; }
        public string? MaKhoanhDat { get; set; }
        public long SoThuTuKhoanhDat { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DTKhongGian { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTich { get; set; }
        public string? MaDoiTuong { get; set; }
        public string? MaDoiTuongKyTruoc { get; set; }
        public string? MucDichSuDung { get; set; }
        public string? MucDichSuDungNLT { get; set; }
        public string? MdSDSatLoBoiDap { get; set; }
        public string? MdSDSoLuongDoiTuong { get; set; }
        public string? MdSDSanGon { get; set; }
        public string? MdSDSanBay { get; set; }
        public string? MdSDLuaChuyenDoi { get; set; }
        public string? MucDichSuDungKyTruoc { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DienTichMucDich { get; set; }
        public long chiTieuId { get; set; }
        public bool? Status { get; set; }
        public long Year { get; set; }
        public byte[] Geo { get; set; }
    }
}

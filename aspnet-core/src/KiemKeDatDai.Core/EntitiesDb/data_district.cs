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
    [Table("Data_District")]
    public class Data_District : FullAuditedEntity<long>
    {
        public string? MaHuyen { get; set; }
        public string? MaTinh { get; set; }
        public string? MaVung { get; set; }
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
        public string? MdSDSanGon { get; set; }
        public string? MdSDSanBay { get; set; }
        public long ChiTieuId { get; set; }
        public long year { get; set; }
        public bool? Status { get; set; }
    }
}

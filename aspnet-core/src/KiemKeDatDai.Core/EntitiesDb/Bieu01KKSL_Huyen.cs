﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu01KKSL_Huyen")]
    public class Bieu01KKSL_Huyen : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        public decimal TongDienTichSatLo { get; set; }
        public decimal SatLoVungBoSong { get; set; }
        public decimal SatLoVungDoiNui { get; set; }
        public decimal SatLoVungBoBien { get; set; }
        public decimal TongDienTichBoiDap { get; set; }
        public decimal BoiDapVungBoSong { get; set; }
        public decimal BoiDapVungBoBien{ get; set; }
        public string MaHuyen  { get; set; }
        public long? HuyenId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu06TKKKQPAN")]
    public class Bieu06TKKKQPAN : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string DonVi { get; set; }
        public string DiaChi { get; set; }
        public decimal DienTichDatQuocPhong { get; set; }
        public decimal DienTichKetHopKhac { get; set; }
        public decimal LoaiDatKetHopKhac { get; set; }
        public decimal DienTichDaDoDac { get; set; }
        public decimal SoGCNDaCap { get; set; }
        public decimal DienTichDaCapGCN { get; set; }
        public string GhiChu { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

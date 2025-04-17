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
    [Table("Bieu06TKKKQPAN_Tinh")]
    public class Bieu06TKKKQPAN_Tinh : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? DonVi { get; set; }
        public string? DiaChi { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDatQuocPhong { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichKetHopKhac { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal LoaiDatKetHopKhac { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDaDoDac { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SoGCNDaCap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDaCapGCN { get; set; }
        public string? GhiChu { get; set; }
        public string? MaTinh { get; set; }
        public long? TinhId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

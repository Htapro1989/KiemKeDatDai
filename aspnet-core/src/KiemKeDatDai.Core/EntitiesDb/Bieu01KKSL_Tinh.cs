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
    [Table("Bieu01KKSL_Tinh")]
    public class Bieu01KKSL_Tinh : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? LoaiDat { get; set; }
        public string? Ma { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTichSatLo { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SatLoVungBoSong { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SatLoVungDoiNui { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SatLoVungBoBien { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTichBoiDap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal BoiDapVungBoSong { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal BoiDapVungBoBien { get; set; }
        public string? MaTinh  { get; set; }
        public long? TinhId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

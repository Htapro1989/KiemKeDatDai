using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu02aKKNLT_Tinh")]
    public class Bieu02aKKNLT_Tinh : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? TenDonVi { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichTheoQDGiaoThue { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichGiaoDat { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichChoThueDat { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichChuaXacDinhGiaoThue { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDoDacTL1000 { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDoDacTL2000 { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDoDacTL5000 { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDoDacTL10000 { get; set; }
        public long SoGCNDaCap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichGCNDaCap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDaBanGiao { get; set; }
        public string? GhiChu { get; set; }
        public long Year { get; set; }
        public string? MaTinh { get; set; }
        public long? TinhId { get; set; }
        public long sequence { get; set; }
        public bool? Active { get; set; }
    }
}

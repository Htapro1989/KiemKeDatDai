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
    [Table("KhoanhDat_KyTruoc")]
    public class KhoanhDat_KyTruoc : FullAuditedEntity<long>
    {
        public string MaXa { get; set; }
        public int STT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTich { get; set; }
        public string MaLoaiDat { get; set; }
        public long LoaiDatId { get; set; }
        public string MaLoaiDatKT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichKT { get; set; }
        public long Year { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DTKhongGian { get; set; }
        public byte[] Geo { get; set; }
    }
}

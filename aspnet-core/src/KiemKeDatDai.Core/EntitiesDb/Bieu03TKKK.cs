using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu03TKKK")]
    public class Bieu03TKKK : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTich { get; set; }
        public string DienTichTheoDVHC { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

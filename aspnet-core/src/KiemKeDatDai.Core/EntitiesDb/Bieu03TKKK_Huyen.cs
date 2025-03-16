using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu03TKKK_Huyen")]
    public class Bieu03TKKK_Huyen : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        public decimal TongDienTich { get; set; }
        public string DienTichTheoDVHC { get; set; }
        public string MaHuyen { get; set; }
        public long? HuyenId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

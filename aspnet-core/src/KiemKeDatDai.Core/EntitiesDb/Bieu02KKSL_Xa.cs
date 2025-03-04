using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu02KKSL_Xa")]
    public class Bieu02KKSL_Xa : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string Ten { get; set; }
        public decimal DienTich { get; set; }
        public string DiaDiem { get; set; }
        public int NamSatLo { get; set; }
        public DateTime NgayLapBieu { get; set; }
        public DateTime NgayDuyet { get; set; }
        public string MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

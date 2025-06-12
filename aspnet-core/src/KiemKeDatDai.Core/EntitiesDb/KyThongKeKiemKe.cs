using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("KyThongKeKiemKe")]
    public class KyThongKeKiemKe : FullAuditedEntity<long>
    {
        public string? Ma { get; set; }
        public string? Name { get; set; }
        public int? LoaiCapDVHC { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("CapDVHC")]
    public class CapDVHC : FullAuditedEntity<long>
    {
        public string MaCapDVHC { get; set; }
        public string Name { get; set; }
        public long Year { get; set; }
        public bool? CapDVHCMin { get; set; }
        public bool? Active { get; set; }
    }
}

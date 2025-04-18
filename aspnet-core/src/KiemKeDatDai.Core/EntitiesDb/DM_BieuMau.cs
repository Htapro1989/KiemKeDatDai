using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("DM_BieuMau")]
    public class DM_BieuMau : FullAuditedEntity<long>
    {
        public string? KyHieu { get; set; }
        public string? NoiDung { get; set; }
        public string? CapDVHC { get; set; }
        public bool? Active { get; set; }
        public long Year { get; set; }
    }
}

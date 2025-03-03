using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("DM_DVCH_BM")]
    public class DM_DVCH_BM : FullAuditedEntity<long>
    {
        public long CapDVHCId { get; set; }
        public string CapDVHCCode { get; set; }
        public long BieuMauId { get; set; }
        public long Year { get; set; }
    }
}

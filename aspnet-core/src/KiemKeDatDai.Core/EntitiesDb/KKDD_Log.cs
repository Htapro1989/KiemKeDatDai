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
    [Table("KKDD_Log")]
    public class KKDD_Log : FullAuditedEntity<long>
    {
        [StringLength(500)]
        public string? TableModify { get; set; }
        public long UserId { get; set; }
        public string? Describle { get; set; }
    }
}

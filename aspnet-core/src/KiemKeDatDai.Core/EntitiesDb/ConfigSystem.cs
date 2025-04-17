using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("ConfigSystem")]
    public class ConfigSystem : FullAuditedEntity<long>
    {
        public int? expired_auth { get; set; }
        public string? JsonConfigSystem { get; set; }
        public bool? Active { get; set; }
    }
}

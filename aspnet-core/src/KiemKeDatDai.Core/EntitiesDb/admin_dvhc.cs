using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("admin_dvhc")]
    public class admin_dvhc : FullAuditedEntity<long>
    {
        public string name { get; set; }
        public long parent_id { get; set; }
        public long level { get; set; }
        public bool activate { get; set; }
        public long year_id { get; set; }
    }
}

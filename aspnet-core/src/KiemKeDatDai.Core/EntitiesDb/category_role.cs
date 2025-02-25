using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("category_role")]
    public class category_role : FullAuditedEntity<long>
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}

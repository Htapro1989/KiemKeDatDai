using Abp.Domain.Entities.Auditing;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("admin_role_group")]
    public class admin_role_group : FullAuditedEntity<long>
    {
        public long user_id { get; set; }
        public long role_id { get; set; }
        public DateTime created_date { get; set; }
    }
}

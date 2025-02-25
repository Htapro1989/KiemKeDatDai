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
    [Table("category_roles")]
    public class category_roles : FullAuditedEntity<long>
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}

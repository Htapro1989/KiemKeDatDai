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
    [Table("admin_token")]
    public class admin_token : FullAuditedEntity<long>
    {
        public long id { get; set; }
        public byte[] token { get; set; }
        public DateTime create_date { get; set; }
        public DateTime expired_date { get; set; }
    }
}

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
    [Table("category_place")]
    public class admin_user : FullAuditedEntity<long>
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public long place_id { get; set; }
        public bool activate { get; set; }
        public long year_id {  get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }

    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("admin_setup")]
    public class admin_setup : FullAuditedEntity<long>
    {
        public long id { get; set; }
        public string expired_token { get; set; }
        public string server_file_upload { get; set; }

    }
}

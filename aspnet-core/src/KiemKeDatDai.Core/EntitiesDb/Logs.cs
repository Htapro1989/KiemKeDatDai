using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Logs")]
    public class Logs : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public int? Action { get; set; }
        public string? Description { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}

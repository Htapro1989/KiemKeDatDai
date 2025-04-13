using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("YKien")]
    public class YKien : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DonViCongTac { get; set; }
        public string NoiDungYKien { get; set; }
        public string NoiDungTraLoi { get; set; }
        public string Url { get; set; }
        public bool? PheDuyet { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public DateTime? NgayTraLoi { get; set; }
    }
}

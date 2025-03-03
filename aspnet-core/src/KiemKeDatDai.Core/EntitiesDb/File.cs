using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("File")]
    public class File : FullAuditedEntity<long>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FilePath { get; set; }
        public long Url { get; set; }
        public bool Active { get; set; }
        public long year { get; set; }
    }
}

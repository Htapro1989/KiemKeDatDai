using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    public class DM_LoaiDat : FullAuditedEntity<long>
    {
        public string MaLoaiDat { get; set; }
        public string ChiTieu { get; set; }
        public long parent_id { get; set; }
        public string parent_code { get; set; }
        public string ThuTuHienThi { get; set; }
        public bool InDam { get; set; }
        public long CapLoaiDat { get; set; }
        public long? sequence { get; set; }
        public int LoaiDat { get; set; }
    }
}

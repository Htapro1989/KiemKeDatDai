using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("DonViHanhChinh")]
    public class DonViHanhChinh : FullAuditedEntity<long>
    {
        public string TenTinh { get; set; }
        public string MaTinh { get; set; }
        public string TenHuyen { get; set; }
        public string MaHuyen { get; set; }
        public string TenXa { get; set; }
        public string MaXa { get; set; }
        public string Name { get; set; }
        public long? Parent_id { get; set; }
        public string Parent_Code { get; set; }
        public long CapDVHCId { get; set; }
        public bool Active { get; set; }
        public long Year { get; set; }
        public int TrangThaiDuyet { get; set; }
    }
}

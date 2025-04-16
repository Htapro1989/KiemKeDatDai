using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Data_BienDong")]
    public class Data_BienDong : FullAuditedEntity<long>
    {
        public string? ThuaTruocBienDong { get; set; }
        public string? ThuaSauBienDong { get; set; }
        public string? TenChuSuDung { get; set; }
        public string? DiaChiThuaDat { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichBienDong { get; set; }
        public string? MDSDTruocBienDong { get; set; }
        public string? MDSDSauBienDong { get; set; }
        public string? DTTruocBienDong { get; set; }
        public string? DTSauBienDong { get; set; }
        public string? SHKDTruocBienDong { get; set; }
        public string? SHKDSauBienDong { get; set; }
        public string? NDThayDoi { get; set; }
        public string? MaXa { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Data_TangGiamKhac")]
    public class Data_TangGiamKhac : FullAuditedEntity<long>
    {
        public long TangGiamKhacId { get; set; }
        public string? MaDVHCCapXa { get; set; }
        public string? MucDichSuDung {  get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichTangGiamKhac { get; set; }
        public long Year { get; set; }
    }
}

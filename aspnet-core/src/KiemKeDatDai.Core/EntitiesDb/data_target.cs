using Abp.Domain.Entities.Auditing;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KiemKeDatDai.EntitiesDb
{
    [Table("Data_Target")]
    public class Data_Target : FullAuditedEntity<long>
    {
        public string? MaDVHCCapXa { get; set; }
        public string? MaKhoanhDat { get; set; }
        public string? MucDichSuDung { get; set; }
        public string? MaDoiTuong { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTich { get; set; }
        public long SoLuong { get; set; }
        public long year { get; set; }
        public long LoaiChiTieu { get; set; }
    }
}

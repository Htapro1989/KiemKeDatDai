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
    [Table("BieuPhuLucIV")]
    public class BieuPhuLucIV : FullAuditedEntity<long>
    {
        public int? SoHieuThua_TruocBienDong { get; set; }
        public int? SoHieuThua_SauBienDong { get; set; }
        public string? TenNguoiSDDat { get; set; }
        public string? DiaChiKhoanhDat { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichCoBienDong { get; set; }
        public string? MaLoaiDat_TruocBienDong { get; set; }
        public string? MaLoaiDat_SauBienDong { get; set; }
        public string? MaLoaiDoiTuong_TruocBienDong { get; set; }
        public string? MaLoaiDoiTuong_SauBienDong { get; set; }
        public string? TTKhoanhDat_TruocBienDong { get; set; }
        public string? TTKhoanhDat_SauBienDong { get; set; }
        public string? NoiDungThayDoi { get; set; }
        public string? MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

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
    [Table("Bieu01bKKNLT_Vung")]
    public class Bieu01bKKNLT_Vung : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string TenDVSDD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTichSuDung { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatNongNghiep { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatPhiNongNghiep { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatSuDungKhongDungMucDich { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTich { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatDangGiaoKhoanTrang { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatDangGiaoChoMuon { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatDangLienDoanh { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatBiLanChiem { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatTranhChap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatGiaoQuanLyNhungChuaSuDung { get; set; }
        public string MaVung { get; set; }
        public long? VungId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

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
    [Table("Bieu01cKKNLT")]
    public class Bieu01cKKNLT : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? LoaiDat { get; set; }
        public string? Ma { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTichQuanLy { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatSuDungDungMucDich { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatSuDungKhongDungMucDich { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TomngDienTich { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatDangGiaoKhoanTrang { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatDangGiaoChoMuon { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatLienDoanh { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatLanChiem { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatTranhChap{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatGiaoQuanLyChuaSuDung{ get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

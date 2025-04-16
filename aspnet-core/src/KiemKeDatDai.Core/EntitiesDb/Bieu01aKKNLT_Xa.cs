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
    [Table("Bieu01aKKNLT_Xa")]
    public class Bieu01aKKNLT_Xa : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? TenDVSDD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongDienTichSuDung { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongSoDatNongNghiep { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatTrongLua { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatTrongCayHangNamKhac { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatTrongCayLauNam { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatRungDacDung { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatRungPhongHo { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatRungSanXuat{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatNuoiTrongThuySan{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CacLoaiDatNongNghiepKhac{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TongSoDatPhiNongNghiep{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatO{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatSXKDPhiNongNghiep { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatCongCong { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatNghiaTrang { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DatMatNuocChuyenDung{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CacLoaiDatPhiNongNghiepKhac{ get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DienTichDatChuaSuDung { get; set; }
        public string? MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

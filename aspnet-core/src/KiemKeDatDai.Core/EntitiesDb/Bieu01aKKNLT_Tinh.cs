using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu01aKKNLT_Tinh")]
    public class Bieu01aKKNLT_Tinh : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string TenDVSDD { get; set; }
        public decimal TongDienTichSuDung { get; set; }
        public decimal TongSoDatNongNghiep { get; set; }
        public decimal DatTrongLua { get; set; }
        public decimal DatTrongCayHangNamKhac { get; set; }
        public decimal DatTrongCayLauNam { get; set; }
        public decimal DatRungDacDung { get; set; }
        public decimal DatRungPhongHo { get; set; }
        public decimal DatRungSanXuat{ get; set; }
        public decimal DatNuoiTrongThuySan{ get; set; }
        public decimal CacLoaiDatNongNghiepKhac{ get; set; }
        public decimal TongSoDatPhiNongNghiep{ get; set; }
        public decimal DatO{ get; set; }
        public decimal DatSXKDPhiNongNghiep { get; set; }
        public decimal DatCongCong { get; set; }
        public decimal DatNghiaTrang { get; set; }
        public decimal DatMatNuocChuyenDung{ get; set; }
        public decimal CacLoaiDatPhiNongNghiepKhac{ get; set; }
        public decimal DienTichDatChuaSuDung { get; set; }
        public string MaTinh { get; set; }
        public long? TinhId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

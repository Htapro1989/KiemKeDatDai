using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu01cKKNLT_Huyen")]
    public class Bieu01cKKNLT_Huyen : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        public decimal TongDienTichQuanLy { get; set; }
        public decimal DatSuDungDungMucDich { get; set; }
        public decimal DatSuDungKhongDungMucDich { get; set; }
        public decimal TomngDienTich { get; set; }
        public decimal DatDangGiaoKhoanTrang { get; set; }
        public decimal DatDangGiaoChoMuon { get; set; }
        public decimal DatLienDoanh { get; set; }
        public decimal DatLanChiem { get; set; }
        public decimal DatTranhChap{ get; set; }
        public decimal DatGiaoQuanLyChuaSuDung{ get; set; }
        public DateTime NgayLapBieu { get; set; }
        public DateTime NgayDuyet { get; set; }
        public long? HuyenId { get; set; }
        public string MaHuyen { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

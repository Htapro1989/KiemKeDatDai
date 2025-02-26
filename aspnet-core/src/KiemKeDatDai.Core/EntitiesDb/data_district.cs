using Abp.Domain.Entities.Auditing;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("data_district")]
    public class data_district : FullAuditedEntity<long>
    {
        public string MaKhoanhDat { get; set; }
        public string MaDVHCCapXa { get; set; }
        public long SoThuTuKhoanhDat { get; set; }
        public decimal DTKhongGian { get; set; }
        public decimal DienTich { get; set; }
        public string MaDoiTuong { get; set; }
        public string MaDoiTuongKyTruoc { get; set; }
        public string MucDichSuDung { get; set; }
        public string MucDichSuDungNLT { get; set; }
        public string MdSDSatLoBoiDap { get; set; }
        public string MdSDSanGon { get; set; }
        public string MdSDSanBay { get; set; }
        public long chiTieuId { get; set; }
        public long year_code { get; set; }
    }
}

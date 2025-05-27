using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    public class ThongKeSoLieuOutputDto
    {
        public int? TongSoTinh { get; set; }
        public int? TongSoTinhChoDuyet { get; set; }
        public int? TongSoTinhHoanThanh { get; set; }
        public int? TongSoHuyen { get; set; }
        public int? TongSoHuyenChoDuyet { get; set; }
        public int? TongSoHuyenHoanThanh { get; set; }
        public int? TongSoXa { get; set; }
        public int? TongSoXaHoanThanh { get; set; }
        public int? VungMienNuiPhiaBac { get; set; }
        public int? VungDongBangSongHong { get; set; }
        public int? VungDuyenHaiMienTrung { get; set; }
        public int? VungTayNguyen { get; set; }
        public int? VungDongNamBo { get; set; }
        public int? VungDongBangSongCuuLong { get; set; }
        public decimal? PhanTramVungMienNuiPhiaBac { get; set; }
        public decimal? PhanTramVungDongBangSongHong { get; set; }
        public decimal? PhanTramVungDuyenHaiMienTrung { get; set; }
        public decimal? PhanTramVungTayNguyen { get; set; }
        public decimal? PhanTramVungDongNamBo { get; set; }
        public decimal? PhanTramVungDongBangSongCuuLong { get; set; }
        public decimal? ChuaNopBaoCao { get; set; }
    }
}

using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    public class BieuPhuLucIIISearchDto : PagedAndFilteredInputDto
    { 
        public long dvhcId { get; set; }
        public long year { get; set; }
    }
    public class BieuPhuLucIIIDto
    {
        //public long STT { get; set; }
        public decimal DienTich { get; set; }
        public string MaLoaiDatHienTrang { get; set; }
        public string MaLoaiDatKyTruoc { get; set; }
        public string MaLoaiDatSuDungKetHop { get; set; }
        public string MaDoiTuongHienTrang { get; set; }
        public string MaDoiTuongKyTruoc { get; set; }
        public string MaKhuVucTongHop { get; set; }
        public string GhiChu { get; set; }
        public string MaXa { get; set; }
    }
    public class BieuPhuLucIIIOutputDto
    {
        public List<BieuPhuLucIIIDto> BieuPhuLucIIIDtos { get; set; }
        public string TenTinh { get; set; }
        public string TenHuyen { get; set; }
        public string TenXa { get; set; }
        public long? Year { get; set; }
    }
}

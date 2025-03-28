using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.Dto;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    public class BieuPhuLucIVSearchDto : PagedAndFilteredInputDto
    {
        public long dvhcId { get; set; }
        public long year { get; set; }
    }
    public class BieuPhuLucIVDto
    {
        public string SHTDTruocBD { get; set; }
        public string SHTDSauBD { get; set; }
        public string TenNguoiSDDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public decimal DienTichBD { get; set; }
        public string MaLoaiDatTruocBD { get; set; }
        public string MaLoaiDatSauBD { get; set; }
        public string MDSDTruocBienDong { get; set; }
        public string MDSDSauBienDong { get; set; }
        public string DTTruocBienDong { get; set; }
        public string DTSauBienDong { get; set; }
        public string NDTD { get; set; }
    }
    public class BieuPhuLucIVOutputDto
    {
        public List<BieuPhuLucIVDto> BieuPhuLucIVDtos { get; set; }
        public string TenTinh { get; set; }
        public string TenHuyen { get; set; }
        public string TenXa { get; set; }
        public long? Year { get; set; }
    }
}

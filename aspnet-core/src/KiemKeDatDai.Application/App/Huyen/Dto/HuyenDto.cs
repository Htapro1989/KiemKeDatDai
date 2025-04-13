using Abp.AutoMapper;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.App.Huyen.Dto
{
    public class DVHCBieu03TKKKDto
    {
        public string TenDVHC { get; set; }
        public string MaDVHC { get; set; }
        public string TenLoaiDat { get; set; }
        public string MaLoaiDat { get; set; }
        public long? Year { get; set; }
        public decimal DienTich { get; set; }
    }
    public class CheckFileDgnReponse
    {
        public bool? IsCheck { get; set; }
        public string Message { get; set; }
    }
}

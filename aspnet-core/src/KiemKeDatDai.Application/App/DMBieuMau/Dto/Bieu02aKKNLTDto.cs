using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.App.DMBieuMau.Dto
{
    public class Bieu02aKKNLTDto: Bieu02aKKNLT
    {
        public decimal TongDienTichDangQL { get; set; }
        public decimal TongDienTich { get; set; }
    }
    public class Bieu02aKKNLT_VungDto : Bieu02aKKNLT_Vung
    {
        public decimal TongDienTichDangQL { get; set; }
        public decimal TongDienTich { get; set; }
    }
    public class Bieu02aKKNLT_TinhDto : Bieu02aKKNLT_Tinh
    {
        public decimal TongDienTichDangQL { get; set; }
        public decimal TongDienTich { get; set; }
    }
    public class Bieu02aKKNLT_TinhInputDto : Bieu02aKKNLT_Tinh
    {
    }
}

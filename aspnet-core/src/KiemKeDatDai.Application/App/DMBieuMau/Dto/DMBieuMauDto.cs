using Abp.AutoMapper;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    [AutoMap(typeof(DM_BieuMau))]
    public class DMBieuMauInputDto : DM_BieuMau
    {
    }
    public class DMBieuMauDto : PagedAndFilteredInputDto
    {
        public string NoiDung { get; set; }
    }
    public class DMBieuMauOuputDto : DM_BieuMau
    {
    }
    public class BieuMauDetailInputDto : PagedAndFilteredInputDto
    {
        public string KyHieu { get; set; }
        public int? CapDVHC { get; set; }
        public long? Year { get; set; }
        public string MaDVHC { get; set; }
    }
}

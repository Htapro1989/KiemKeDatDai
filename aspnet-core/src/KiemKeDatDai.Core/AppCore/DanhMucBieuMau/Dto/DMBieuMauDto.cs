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
}

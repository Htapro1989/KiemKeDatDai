using Abp.Application.Services;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IBieuPhuLucAppService : IApplicationService
    {
        Task<CommonResponseDto> GetBieuPL3ByDVHC(BieuPhuLucIIISearchDto input);
        Task<CommonResponseDto> GetBieuPL4ByDVHC(BieuPhuLucIVSearchDto input);
    }
}

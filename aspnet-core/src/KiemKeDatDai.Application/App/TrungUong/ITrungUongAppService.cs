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
    public interface ITrungUongAppService : IApplicationService
    {
        Task<CommonResponseDto> DuyetBaoCaoTinh(long tinhId);
        Task<CommonResponseDto> HuyDuyetBaoCaoTinh(long tinhId);
    }
}

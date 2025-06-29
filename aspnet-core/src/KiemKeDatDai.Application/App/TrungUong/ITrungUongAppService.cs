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
        Task<CommonResponseDto> DuyetBaoCaoTinh(string ma, long year, int loaiCapDvhc);
        Task<CommonResponseDto> HuyDuyetBaoCaoTinh(string ma, long year, int loaiCapDvhc);
    }
}

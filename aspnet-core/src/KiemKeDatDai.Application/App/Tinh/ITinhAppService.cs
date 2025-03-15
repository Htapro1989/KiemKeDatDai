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
    public interface ITinhAppService : IApplicationService
    {
        Task<CommonResponseDto> DuyetBaoCaoHuyen(string ma, long year);
        Task<CommonResponseDto> HuyDuyetBaoCaoHuyen(string ma, long year);
    }
}

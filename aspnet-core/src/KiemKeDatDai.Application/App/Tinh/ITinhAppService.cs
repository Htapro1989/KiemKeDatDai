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
        Task<CommonResponseDto> DuyetBaoCaoHuyen(long huyenId);
        Task<CommonResponseDto> HuyDuyetBaoCaoHuyen(long huyenId);
        Task<CommonResponseDto> NopBaoCaoTrungUong();
    }
}

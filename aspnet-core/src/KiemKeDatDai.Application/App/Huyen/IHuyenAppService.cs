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
    public interface IHuyenAppService : IApplicationService
    {
        Task<CommonResponseDto> DuyetBaoCaoXa(string ma, long year);
        Task<CommonResponseDto> DuyetAllXaTrongHuyen(string maHuyen, long year);
        Task<CommonResponseDto> HuyDuyetBaoCaoXa(string ma, long year);
        Task<CommonResponseDto> HuyDuyetAllXaTrongHuyen(string maHuyen, long year);
    }
}

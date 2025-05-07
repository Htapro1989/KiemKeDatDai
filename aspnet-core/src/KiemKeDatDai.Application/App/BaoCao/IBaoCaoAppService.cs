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
    public interface IBaoCaoAppService : IApplicationService
    {
        Task<CommonResponseDto> NopBaoCao(long year);
        Task<CommonResponseDto> ThongKeSoLieu();
        Task<CommonResponseDto> DeleteAllDataXa(long year, string ma);
    }
}

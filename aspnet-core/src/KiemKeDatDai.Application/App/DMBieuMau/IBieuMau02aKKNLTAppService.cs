using Abp.Application.Services;
using KiemKeDatDai.App.DMBieuMau.Dto;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IBieuMau02aKKNLTAppService : IApplicationService
    {
        Task<CommonResponseDto> GetByDVHC(long dvhcId, long year);
        Task<CommonResponseDto> CreateOrUpdate(Bieu02aKKNLT_TinhDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}

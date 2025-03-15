using Abp.Application.Services;
using KiemKeDatDai.App.Huyen.Dto;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IDMBieuMauAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(DMBieuMauDto obj);
        Task<CommonResponseDto> GetByDVHC(long dvhcId);
        Task<CommonResponseDto> CreateOrUpdate(DMBieuMauInputDto input);
        Task<CommonResponseDto> Delete(long id);
        Task<CommonResponseDto> GetDetailBieuByKyHieu(BieuMauDetailInputDto input);
    }
}

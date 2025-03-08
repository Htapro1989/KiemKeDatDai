using Abp.Application.Services;
using KiemKeDatDai.Dto;
using KiemKeDatDai.ApplicationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IDonViHanhChinhAppService : IApplicationService
    {
        Task<CommonResponseDto> GetByUser(DVHCInput input);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> CreateOrUpdate(DVHCInputDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}
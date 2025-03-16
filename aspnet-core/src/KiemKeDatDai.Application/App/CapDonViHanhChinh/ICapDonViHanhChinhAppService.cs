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
    public interface ICapDonViHanhChinhAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(CapDVHCDto input);
        Task<CommonResponseDto> GetById(long Id);
        Task<CommonResponseDto> CreateOrUpdate(CapDVHCInputDto input);
        Task<CommonResponseDto> Delete(long id);
        Task<CommonResponseDto> GetCapDVHC();
    }
}

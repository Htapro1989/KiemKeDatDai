using Abp.Application.Services;
using KiemKeDatDai.AppCore.DanhMucDVHC.Dto;
using KiemKeDatDai.AppCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.AppCore.DanhMucDVHC
{
    public interface IDonViHanhChinhAppService : IApplicationService
    {
        Task<CommonResponseDto> GetByUser(long userId);
        Task<CommonResponseDto> CreateOrUpdate(DVHCInputDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}

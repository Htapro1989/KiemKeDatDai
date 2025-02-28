using Abp.Application.Services;
using KiemKeDatDai.AppCore.Log.Dto;
using KiemKeDatDai.AppCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.AppCore.Log
{
    public interface ILogAppService : IApplicationService
    {
        CommonResponseDto GetAll(LogDto input);
        CommonResponseDto Create(LogInputDto input);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> Delete(long id);
    }
}

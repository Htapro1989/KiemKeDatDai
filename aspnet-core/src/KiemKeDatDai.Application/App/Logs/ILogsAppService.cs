using Abp.Application.Services;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiemKeDatDai.CommonEnum;

namespace KiemKeDatDai.RisApplication
{
    public interface ILogsAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(LogsDto input);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> CreateOrUpdate(LogsInputDto input);
        Task<CommonResponseDto> Delete(long id);
        /// <summary>
        /// Ghi log thông tin
        /// </summary>
        /// <param name="action"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<CommonResponseDto> LogInfo(HANH_DONG action,string message);
    }
}

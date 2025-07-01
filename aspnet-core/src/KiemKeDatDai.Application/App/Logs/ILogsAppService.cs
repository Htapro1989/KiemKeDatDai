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
    public interface ILogsAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(LogsDto input);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> CreateOrUpdate(LogsInputDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}

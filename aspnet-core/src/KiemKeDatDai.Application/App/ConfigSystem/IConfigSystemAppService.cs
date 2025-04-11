using Abp.Application.Services;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IConfigSystemAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(string filter);
        Task<CommonResponseDto> GetById(long dvhcId);
        Task<CommonResponseDto> CreateOrUpdate(ConfigSytemInputDto input);
        Task<CommonResponseDto> Delete(long id);
        Task<int> GetByActive();
    }
}

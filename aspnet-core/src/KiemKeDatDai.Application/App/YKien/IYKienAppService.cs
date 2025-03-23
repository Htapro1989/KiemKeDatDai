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
    public interface IYKienAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(YKienDto input);
        Task<CommonResponseDto> GetById(long dvhcId);
        Task<CommonResponseDto> CreateOrUpdate(YKienInputDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}

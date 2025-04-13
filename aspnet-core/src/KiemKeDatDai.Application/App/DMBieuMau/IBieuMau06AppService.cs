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
    public interface IBieuMau06AppService : IApplicationService
    {
        Task<CommonResponseDto> GetByDVHC(long dvhcId, long year);
        Task<CommonResponseDto> CreateOrUpdate(Bieu06TKKKQPAN_TinhDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}

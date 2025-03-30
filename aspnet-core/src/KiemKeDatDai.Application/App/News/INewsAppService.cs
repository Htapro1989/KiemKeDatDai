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
    public interface INewsAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(int type);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> CreateOrUpdate(NewsUploadDto input);
        Task<CommonResponseDto> Delete(long id);
    }
}

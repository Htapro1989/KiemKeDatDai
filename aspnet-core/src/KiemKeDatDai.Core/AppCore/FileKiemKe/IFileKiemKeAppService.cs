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
    public interface IFileKiemKeAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(FileKiemKeInputDto obj);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> CreateOrUpdate(FileKiemKeInputDto input);
        Task<CommonResponseDto> Delete(long id);
        Task<CommonResponseDto> UploadFile(FileUploadInputDto input);
    }
}

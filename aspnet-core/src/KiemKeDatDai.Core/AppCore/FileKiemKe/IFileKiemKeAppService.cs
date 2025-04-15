using Abp.Application.Services;
using KiemKeDatDai.ApplicationDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IFileKiemKeAppService : IApplicationService
    {
        Task<CommonResponseDto> UploadFile(FileUploadInputDto input);
        Task<long> CreateFile(IFormFile file, long? dvhcId, long year, string maDvhc = "");
    }
}

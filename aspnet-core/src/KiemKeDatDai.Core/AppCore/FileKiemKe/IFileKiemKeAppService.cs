using Abp.Application.Services;
using KiemKeDatDai.ApplicationDto;

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
    }
}

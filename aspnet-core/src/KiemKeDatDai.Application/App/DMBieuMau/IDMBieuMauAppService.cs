using Abp.Application.Services;
using KiemKeDatDai.App.Huyen.Dto;
using KiemKeDatDai.AppCore.Utility;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IDMBieuMauAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(DMBieuMauDto obj);
        Task<CommonResponseDto> GetAllAdmin(DMBieuMauDto input);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> GetByDVHC(long dvhcId);
        Task<CommonResponseDto> CreateOrUpdate(DMBieuMauInputDto input);
        Task<CommonResponseDto> Delete(long id);
        Task<CommonResponseDto> GetDetailBieuByKyHieu(BieuMauDetailInputDto input);
        Task<FileStreamResult> DownloadBieuMau(BieuMauDetailInputDto input);
        Task<FileStreamResult> DownloadTemplate(string mabieu);
        List<DropDownListDVHCDto> GetKyHieuBieuMau();
    }
}

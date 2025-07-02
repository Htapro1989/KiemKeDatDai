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
    public interface IBaoCaoAppService : IApplicationService
    {
        Task<CommonResponseDto> NopBaoCao(long year, string ma);
        Task<CommonResponseDto> ThongKeSoLieu(long year);
        Task<CommonResponseDto> DeleteAllDataXa(long year, string ma);
        Task<CommonResponseDto> ReportNumberXaByDate(DateTime fromDate, DateTime toDate);
        Task<CommonResponseDto> DeleteAllBieuHuyen(long year, string maHuyen);
    }
}

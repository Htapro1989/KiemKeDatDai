﻿using Abp.Application.Services;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IHuyenAppService : IApplicationService
    {
        Task<CommonResponseDto> DuyetBaoCaoXa(long xaId, long year);
        Task<CommonResponseDto> HuyDuyetBaoCaoXa(long xaId, long year);
        Task<CommonResponseDto> NopBaoCaoTinh();
    }
}

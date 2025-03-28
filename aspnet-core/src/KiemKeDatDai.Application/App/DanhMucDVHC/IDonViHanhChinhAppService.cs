﻿using Abp.Application.Services;
using KiemKeDatDai.Dto;
using KiemKeDatDai.ApplicationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication
{
    public interface IDonViHanhChinhAppService : IApplicationService
    {
        Task<CommonResponseDto> GetAll(DVHCDto input);
        Task<CommonResponseDto> GetByUser(DVHCInput input);
        Task<CommonResponseDto> GetById(long id);
        Task<CommonResponseDto> CreateOrUpdate(DVHCInputDto input);
        Task<CommonResponseDto> Delete(long id);
        Task<CommonResponseDto> BaoCaoDVHC(BaoCaoInPutDto input);
        Task<CommonResponseDto> GetDropDownVung();
        Task<CommonResponseDto> GetDropDownTinhByVungId(long vungId);
        Task<CommonResponseDto> GetDropDownTinhByMaVung(string ma);
        Task<CommonResponseDto> GetDropDownTinh();
        Task<CommonResponseDto> GetDropDownHuyenByTinhId(long tinhId);
        Task<CommonResponseDto> GetDropDownHuyenByMaTinh(string ma);
        Task<CommonResponseDto> GetDropDownXaByHuyenId(long huyenId);
        Task<CommonResponseDto> GetDropDownXaByMaHuyen(string ma);
    }
}
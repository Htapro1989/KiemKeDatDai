using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using KiemKeDatDai;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Abp.Threading;
using KiemKeDatDai.Sessions;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using KiemKeDatDai.RisApplication;
using static KiemKeDatDai.CommonEnum;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Transactions;

namespace KiemKeDatDai.App.DMBieuMau
{
    public class TinhAppService : KiemKeDatDaiAppServiceBase, ITinhAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Tinh, long> _bieu01TKKK_TinhRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public TinhAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Tinh, long> bieu01TKKK_TinhRepos,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _dvhcRepos = dvhcRepos;
            _dmKyThongKeKiemKeRepos = dmKyThongKeKiemKeRepos;
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_TinhRepos = bieu01TKKK_TinhRepos;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize]
        public async Task<CommonResponseDto> DuyetBaoCaoHuyen(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(currentUser.DonViHanhChinhId.Value);
                    if (objdata != null)
                    {
                        if (objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                        {
                            commonResponseDto.Message = "Tỉnh đã được duyệt, không thể duyệt huyện";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var huyen = await _dvhcRepos.FirstOrDefaultAsync(x=>x.Ma == ma);
                            if (huyen != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, year, (int)HAM_DUYET.DUYET);

                                #region cập nhật DVHC huyện sau khi duyệt huyện
                                huyen.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                huyen.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(huyen);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Huyện này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC tỉnh sau khi duyệt huyện
                            if (objdata.SoDVHCDaDuyet == null)
                            {
                                objdata.SoDVHCDaDuyet = 1;
                            }
                            else
                            {
                                objdata.SoDVHCDaDuyet++;
                            }
                            if (objdata.SoDVHCCon == null)
                            {
                                objdata.SoDVHCCon = await _dvhcRepos.CountAsync(x => x.MaTinh == currentUser.DonViHanhChinhCode);
                            }
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Tỉnh này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> HuyDuyetBaoCaoHuyen(string ma, long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(currentUser.DonViHanhChinhId.Value);
                    if (objdata != null)
                    {
                        if (objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET)
                        {
                            commonResponseDto.Message = "Tỉnh đã được duyệt, không thể hủy duyệt huyện";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }
                        else
                        {
                            var huyen = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
                            if (huyen != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuTinh(objdata, ma, year, (int)HAM_DUYET.HUY);

                                #region cập nhật DVHC xã sau khi duyệt xã
                                huyen.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                huyen.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(huyen);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Huyện này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                                return commonResponseDto;
                            }

                            #region cập nhật DVHC tỉnh sau khi duyệt huyện
                            objdata.SoDVHCDaDuyet--;
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Tỉnh này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }

        private async Task<CommonResponseDto> CreateOrUpdateBieuTinh(DonViHanhChinh tinh, string maHuyen, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            var data_huyen = await _bieu01TKKK_HuyenRepos.GetAllListAsync(x => x.MaHuyen == maHuyen && x.Year == year);
            if (data_huyen != null)
            {
                await CreateOrUpdateBieu01TKKK_Tinh(data_huyen, tinh.Id, tinh.MaTinh, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu huyện biểu 01TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                return commonResponseDto;
            }

            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        #region Biểu 01TKKK
        private async Task CreateOrUpdateBieu01TKKK_Tinh(List<Bieu01TKKK_Huyen> huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            var data_tinh = await _bieu01TKKK_TinhRepos.GetAllListAsync(x => x.MaTinh == maTinh && x.Year == year);
            if (data_tinh.Count == 0)
            {
                foreach (var item in huyen)
                {
                    //Tạo các bản ghi tỉnh tương ứng với bản ghi huyện
                    await CreateBieu01TKKK_Tinh(item, tinhId, maTinh);
                }
            }
            else
            {
                foreach (var item in huyen)
                {
                    //Cập nhật các bản ghi tỉnh tương ứng với bản ghi huyện
                    await UpdateBieu01TKKK_Tinh(item, tinhId, maTinh, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu01TKKK_Tinh(Bieu01TKKK_Huyen huyen, long tinhId, string maTinh)
        {
            try
            {
                var objtinh = new Bieu01TKKK_Tinh()
                {
                    STT = huyen.STT,
                    LoaiDat = huyen.LoaiDat,
                    Ma = huyen.Ma,
                    TongDienTichDVHC = huyen.TongDienTichDVHC,
                    TongSoTheoDoiTuongSuDung = huyen.TongSoTheoDoiTuongSuDung,
                    CaNhanTrongNuoc_CNV = huyen.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = huyen.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = huyen.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = huyen.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = huyen.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = huyen.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = huyen.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = huyen.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = huyen.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = huyen.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = huyen.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = huyen.ToChucKinhTeVonNuocNgoai_TVN,
                    TongSoTheoDoiTuongDuocGiaoQuanLy = huyen.TongSoTheoDoiTuongDuocGiaoQuanLy,
                    CoQuanNhaNuoc_TCQ = huyen.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = huyen.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = huyen.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = huyen.CongDongDanCu_CDQ,
                    TinhId = tinhId,
                    MaTinh = maTinh,
                    Year = huyen.Year,
                    Active = true,
                };
                await _bieu01TKKK_TinhRepos.InsertAsync(objtinh);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu01TKKK_Tinh(Bieu01TKKK_Huyen huyen, long tinhId, string maTinh, long year, int hamduyet)
        {
            try
            {
                var objtinh = await _bieu01TKKK_TinhRepos.FirstOrDefaultAsync(x => x.MaTinh == maTinh && x.Year == year);
                if (objtinh.Id > 0)
                {
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objtinh.TongDienTichDVHC += huyen.TongDienTichDVHC;
                        objtinh.TongSoTheoDoiTuongSuDung += huyen.TongSoTheoDoiTuongSuDung;
                        objtinh.CaNhanTrongNuoc_CNV += huyen.CaNhanTrongNuoc_CNV;
                        objtinh.NguoiVietNamONuocNgoai_CNN += huyen.NguoiVietNamONuocNgoai_CNN;
                        objtinh.CoQuanNhaNuoc_TCN += huyen.CoQuanNhaNuoc_TCN;
                        objtinh.DonViSuNghiep_TSN += huyen.DonViSuNghiep_TSN;
                        objtinh.ToChucXaHoi_TXH += huyen.ToChucXaHoi_TXH;
                        objtinh.ToChucKinhTe_TKT += huyen.ToChucKinhTe_TKT;
                        objtinh.ToChucKhac_TKH += huyen.ToChucKhac_TKH;
                        objtinh.ToChucTonGiao_TTG += huyen.ToChucTonGiao_TTG;
                        objtinh.CongDongDanCu_CDS += huyen.CongDongDanCu_CDS;
                        objtinh.ToChucNuocNgoai_TNG += huyen.ToChucNuocNgoai_TNG;
                        objtinh.NguoiGocVietNamONuocNgoai_NGV += huyen.NguoiGocVietNamONuocNgoai_NGV;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN += huyen.ToChucKinhTeVonNuocNgoai_TVN;
                        objtinh.TongSoTheoDoiTuongDuocGiaoQuanLy += huyen.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objtinh.CoQuanNhaNuoc_TCQ += huyen.CoQuanNhaNuoc_TCQ;
                        objtinh.DonViSuNghiep_TSQ += huyen.DonViSuNghiep_TSQ;
                        objtinh.ToChucKinhTe_KTQ += huyen.ToChucKinhTe_KTQ;
                        objtinh.CongDongDanCu_CDQ += huyen.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objtinh.TongDienTichDVHC -= huyen.TongDienTichDVHC;
                        objtinh.TongSoTheoDoiTuongSuDung -= huyen.TongSoTheoDoiTuongSuDung;
                        objtinh.CaNhanTrongNuoc_CNV -= huyen.CaNhanTrongNuoc_CNV;
                        objtinh.NguoiVietNamONuocNgoai_CNN -= huyen.NguoiVietNamONuocNgoai_CNN;
                        objtinh.CoQuanNhaNuoc_TCN -= huyen.CoQuanNhaNuoc_TCN;
                        objtinh.DonViSuNghiep_TSN -= huyen.DonViSuNghiep_TSN;
                        objtinh.ToChucXaHoi_TXH -= huyen.ToChucXaHoi_TXH;
                        objtinh.ToChucKinhTe_TKT -= huyen.ToChucKinhTe_TKT;
                        objtinh.ToChucKhac_TKH -= huyen.ToChucKhac_TKH;
                        objtinh.ToChucTonGiao_TTG -= huyen.ToChucTonGiao_TTG;
                        objtinh.CongDongDanCu_CDS -= huyen.CongDongDanCu_CDS;
                        objtinh.ToChucNuocNgoai_TNG -= huyen.ToChucNuocNgoai_TNG;
                        objtinh.NguoiGocVietNamONuocNgoai_NGV -= huyen.NguoiGocVietNamONuocNgoai_NGV;
                        objtinh.ToChucKinhTeVonNuocNgoai_TVN -= huyen.ToChucKinhTeVonNuocNgoai_TVN;
                        objtinh.TongSoTheoDoiTuongDuocGiaoQuanLy -= huyen.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objtinh.CoQuanNhaNuoc_TCQ -= huyen.CoQuanNhaNuoc_TCQ;
                        objtinh.DonViSuNghiep_TSQ -= huyen.DonViSuNghiep_TSQ;
                        objtinh.ToChucKinhTe_KTQ -= huyen.ToChucKinhTe_KTQ;
                        objtinh.CongDongDanCu_CDQ -= huyen.CongDongDanCu_CDQ;
                    }
                    await _bieu01TKKK_TinhRepos.UpdateAsync(objtinh);
                }
                else
                {
                    await CreateBieu01TKKK_Tinh(huyen, tinhId, maTinh);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion
    }
}

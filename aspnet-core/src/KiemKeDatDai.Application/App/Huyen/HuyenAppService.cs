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
    public class HuyenAppService : KiemKeDatDaiAppServiceBase, IHuyenAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public HuyenAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,
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
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }


        [AbpAuthorize]
        public async Task<CommonResponseDto> DuyetBaoCaoXa(long xaId, long year)
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
                            commonResponseDto.Message = "Huyện đã được duyệt, không thể duyệt xã";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        }
                        else
                        {
                            var xa = await _dvhcRepos.FirstOrDefaultAsync(xaId);
                            if (xa != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuHuyen(objdata, xaId, year, (int)HAM_DUYET.DUYET);

                                #region cập nhật DVHC xã sau khi duyệt xã
                                xa.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                xa.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(xa);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Xã này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            }

                            #region cập nhật DVHC huyện sau khi duyệt xã
                            objdata.SoDVHCDaDuyet++;
                            if (objdata.SoDVHCCon == null)
                            {
                                objdata.SoDVHCCon = await _dvhcRepos.GetAll().Where(x => x.Parent_id == currentUser.DonViHanhChinhId.Value).CountAsync();
                            }
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Huyện này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
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
        public async Task<CommonResponseDto> HuyDuyetBaoCaoXa(long xaId, long year)
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
                            commonResponseDto.Message = "Huyện đã được duyệt, không thể hủy duyệt xã";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        }
                        else
                        {
                            var xa = await _dvhcRepos.FirstOrDefaultAsync(xaId);
                            if (xa != null)
                            {
                                //gọi hàm update biểu huyện
                                commonResponseDto = await CreateOrUpdateBieuHuyen(objdata, xaId, year, (int)HAM_DUYET.HUY);

                                #region cập nhật DVHC xã sau khi duyệt xã
                                xa.TrangThaiDuyet = (int)TRANG_THAI_DUYET.DA_DUYET;
                                xa.NgayDuyet = DateTime.Now;
                                await _dvhcRepos.UpdateAsync(xa);
                                #endregion
                            }
                            else
                            {
                                commonResponseDto.Message = "Xã này không tồn tại";
                                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                            }

                            #region cập nhật DVHC huyện sau khi duyệt xã
                            objdata.SoDVHCDaDuyet--;
                            await _dvhcRepos.UpdateAsync(objdata);
                            #endregion
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "Huyện này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
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
        public async Task<CommonResponseDto> NopBaoCaoTinh()
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
                        if (objdata.SoDVHCDaDuyet < objdata.SoDVHCCon)
                        {
                            commonResponseDto.Message = "Chưa duyệt hết các xã trong huyện";
                            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                        }
                        objdata.NgayGui = DateTime.Now;
                        objdata.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHO_DUYET;
                        await _dvhcRepos.UpdateAsync(objdata);
                    }
                    else
                    {
                        commonResponseDto.Message = "Huyện này không tồn tại";
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
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

        private async Task<CommonResponseDto> CreateOrUpdateBieuHuyen(DonViHanhChinh huyen, long xaId, long year, int hamduyet)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            var data_xa = await _bieu01TKKK_XaRepos.GetAllListAsync(x => x.XaId == xaId && x.Year == year);
            if (data_xa.Count > 0)
            {
                await CreateOrUpdateBieu01TKKK_Huyen(data_xa, huyen.Id, huyen.MaHuyen, year, hamduyet);
            }
            else
            {
                commonResponseDto.Message = "Dữ liệu xã biểu 01TKKK không tồn tại";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
            }

            commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
            commonResponseDto.Message = "Thành Công";
            return commonResponseDto;
        }

        #region Biểu 01TKKK
        private async Task CreateOrUpdateBieu01TKKK_Huyen(List<Bieu01TKKK_Xa> xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            var data_huyen = await _bieu01TKKK_HuyenRepos.GetAllListAsync(x => x.HuyenId == huyenId && x.Year == year);
            if (data_huyen.Count == 0)
            {
                foreach (var item in xa)
                {
                    //Tạo các bản ghi huyện tương ứng với bản ghi xã
                    await CreateBieu01TKKK_Huyen(item, huyenId, maHuyen);
                }
            }
            else
            {
                foreach (var item in xa)
                {
                    //Cập nhật các bản ghi huyện tương ứng với bản ghi xã
                    await UpdateBieu01TKKK_Huyen(item, huyenId, maHuyen, year, hamduyet);
                }
            }
        }

        private async Task CreateBieu01TKKK_Huyen(Bieu01TKKK_Xa xa, long huyenId, string maHuyen)
        {
            try
            {
                var objhuyen = new Bieu01TKKK_Huyen()
                {
                    STT = xa.STT,
                    LoaiDat = xa.LoaiDat,
                    Ma = xa.Ma,
                    TongDienTichDVHC = xa.TongDienTichDVHC,
                    TongSoTheoDoiTuongSuDung = xa.TongSoTheoDoiTuongSuDung,
                    CaNhanTrongNuoc_CNV = xa.CaNhanTrongNuoc_CNV,
                    NguoiVietNamONuocNgoai_CNN = xa.NguoiVietNamONuocNgoai_CNN,
                    CoQuanNhaNuoc_TCN = xa.CoQuanNhaNuoc_TCN,
                    DonViSuNghiep_TSN = xa.DonViSuNghiep_TSN,
                    ToChucXaHoi_TXH = xa.ToChucXaHoi_TXH,
                    ToChucKinhTe_TKT = xa.ToChucKinhTe_TKT,
                    ToChucKhac_TKH = xa.ToChucKhac_TKH,
                    ToChucTonGiao_TTG = xa.ToChucTonGiao_TTG,
                    CongDongDanCu_CDS = xa.CongDongDanCu_CDS,
                    ToChucNuocNgoai_TNG = xa.ToChucNuocNgoai_TNG,
                    NguoiGocVietNamONuocNgoai_NGV = xa.NguoiGocVietNamONuocNgoai_NGV,
                    ToChucKinhTeVonNuocNgoai_TVN = xa.ToChucKinhTeVonNuocNgoai_TVN,
                    TongSoTheoDoiTuongDuocGiaoQuanLy = xa.TongSoTheoDoiTuongDuocGiaoQuanLy,
                    CoQuanNhaNuoc_TCQ = xa.CoQuanNhaNuoc_TCQ,
                    DonViSuNghiep_TSQ = xa.DonViSuNghiep_TSQ,
                    ToChucKinhTe_KTQ = xa.ToChucKinhTe_KTQ,
                    CongDongDanCu_CDQ = xa.CongDongDanCu_CDQ,
                    HuyenId = huyenId,
                    MaHuyen = maHuyen,
                    Year = xa.Year,
                    Active = true,
                };
                await _bieu01TKKK_HuyenRepos.InsertAsync(objhuyen);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        private async Task UpdateBieu01TKKK_Huyen(Bieu01TKKK_Xa xa, long huyenId, string maHuyen, long year, int hamduyet)
        {
            try
            {
                var objhuyen = await _bieu01TKKK_HuyenRepos.FirstOrDefaultAsync(x => x.HuyenId == huyenId && x.Ma == xa.Ma && x.Year == year);
                if (objhuyen.Id > 0)
                {
                    //update duyệt xã
                    if (hamduyet == (int)HAM_DUYET.DUYET)
                    {
                        objhuyen.TongDienTichDVHC += xa.TongDienTichDVHC;
                        objhuyen.TongSoTheoDoiTuongSuDung += xa.TongSoTheoDoiTuongSuDung;
                        objhuyen.CaNhanTrongNuoc_CNV += xa.CaNhanTrongNuoc_CNV;
                        objhuyen.NguoiVietNamONuocNgoai_CNN += xa.NguoiVietNamONuocNgoai_CNN;
                        objhuyen.CoQuanNhaNuoc_TCN += xa.CoQuanNhaNuoc_TCN;
                        objhuyen.DonViSuNghiep_TSN += xa.DonViSuNghiep_TSN;
                        objhuyen.ToChucXaHoi_TXH += xa.ToChucXaHoi_TXH;
                        objhuyen.ToChucKinhTe_TKT += xa.ToChucKinhTe_TKT;
                        objhuyen.ToChucKhac_TKH += xa.ToChucKhac_TKH;
                        objhuyen.ToChucTonGiao_TTG += xa.ToChucTonGiao_TTG;
                        objhuyen.CongDongDanCu_CDS += xa.CongDongDanCu_CDS;
                        objhuyen.ToChucNuocNgoai_TNG += xa.ToChucNuocNgoai_TNG;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV += xa.NguoiGocVietNamONuocNgoai_NGV;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN += xa.ToChucKinhTeVonNuocNgoai_TVN;
                        objhuyen.TongSoTheoDoiTuongDuocGiaoQuanLy += xa.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objhuyen.CoQuanNhaNuoc_TCQ += xa.CoQuanNhaNuoc_TCQ;
                        objhuyen.DonViSuNghiep_TSQ += xa.DonViSuNghiep_TSQ;
                        objhuyen.ToChucKinhTe_KTQ += xa.ToChucKinhTe_KTQ;
                        objhuyen.CongDongDanCu_CDQ += xa.CongDongDanCu_CDQ;
                    }
                    //update huỷ duyệt xã
                    else
                    {
                        objhuyen.TongDienTichDVHC -= xa.TongDienTichDVHC;
                        objhuyen.TongSoTheoDoiTuongSuDung -= xa.TongSoTheoDoiTuongSuDung;
                        objhuyen.CaNhanTrongNuoc_CNV -= xa.CaNhanTrongNuoc_CNV;
                        objhuyen.NguoiVietNamONuocNgoai_CNN -= xa.NguoiVietNamONuocNgoai_CNN;
                        objhuyen.CoQuanNhaNuoc_TCN -= xa.CoQuanNhaNuoc_TCN;
                        objhuyen.DonViSuNghiep_TSN -= xa.DonViSuNghiep_TSN;
                        objhuyen.ToChucXaHoi_TXH -= xa.ToChucXaHoi_TXH;
                        objhuyen.ToChucKinhTe_TKT -= xa.ToChucKinhTe_TKT;
                        objhuyen.ToChucKhac_TKH -= xa.ToChucKhac_TKH;
                        objhuyen.ToChucTonGiao_TTG -= xa.ToChucTonGiao_TTG;
                        objhuyen.CongDongDanCu_CDS -= xa.CongDongDanCu_CDS;
                        objhuyen.ToChucNuocNgoai_TNG -= xa.ToChucNuocNgoai_TNG;
                        objhuyen.NguoiGocVietNamONuocNgoai_NGV -= xa.NguoiGocVietNamONuocNgoai_NGV;
                        objhuyen.ToChucKinhTeVonNuocNgoai_TVN -= xa.ToChucKinhTeVonNuocNgoai_TVN;
                        objhuyen.TongSoTheoDoiTuongDuocGiaoQuanLy -= xa.TongSoTheoDoiTuongDuocGiaoQuanLy;
                        objhuyen.CoQuanNhaNuoc_TCQ -= xa.CoQuanNhaNuoc_TCQ;
                        objhuyen.DonViSuNghiep_TSQ -= xa.DonViSuNghiep_TSQ;
                        objhuyen.ToChucKinhTe_KTQ -= xa.ToChucKinhTe_KTQ;
                        objhuyen.CongDongDanCu_CDQ -= xa.CongDongDanCu_CDQ;
                    }
                    await _bieu01TKKK_HuyenRepos.UpdateAsync(objhuyen);
                }
                else
                {
                    await CreateBieu01TKKK_Huyen(xa, huyenId, maHuyen);
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

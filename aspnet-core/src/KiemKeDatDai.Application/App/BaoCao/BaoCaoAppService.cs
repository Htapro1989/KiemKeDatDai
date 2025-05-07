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
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai.RisApplication
{
    public class BaoCaoAppService : KiemKeDatDaiAppServiceBase, IBaoCaoAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<KyThongKeKiemKe, long> _dmKyThongKeKiemKeRepos;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<Bieu01TKKK_Huyen, long> _bieu01TKKK_HuyenRepos;
        private readonly IRepository<Bieu01TKKK_Xa, long> _bieu01TKKK_XaRepos;
        private readonly IRepository<Bieu02TKKK_Xa, long> _bieu02TKKK_XaRepos;
        private readonly IRepository<Bieu04TKKK_Xa, long> _bieu04TKKK_XaRepos;
        private readonly IRepository<Bieu05TKKK_Xa, long> _bieu05TKKK_XaRepos;
        private readonly IRepository<Bieu01aKKNLT_Xa, long> _bieu01aKKNLT_XaRepos;
        private readonly IRepository<Bieu01bKKNLT_Xa, long> _bieu01bKKNLT_XaRepos;
        private readonly IRepository<Bieu01cKKNLT_Xa, long> _bieu01cKKNLT_XaRepos;
        private readonly IRepository<Bieu01KKSL_Xa, long> _bieu01KKSL_XaRepos;
        private readonly IRepository<Bieu02KKSL_Xa, long> _bieu02KKSL_XaRepos;
        private readonly IRepository<BieuPhuLucIII, long> _bieuPhuLucIIIRepos;
        private readonly IRepository<BieuPhuLucIV, long> _bieuPhuLucIVRepos;
        private readonly IRepository<KhoanhDat_KyTruoc, long> _khoanhDat_KyTruocRepos;
        private readonly IRepository<Data_Target, long> _data_TargetRepos;
        private readonly IRepository<SoLieuKyTruoc, long> _soLieuKyTruocRepos;
        private readonly IRepository<Data_Commune, long> _data_CommuneRepos;
        private readonly IRepository<Data_TangGiamKhac, long> _data_TangGiamKhacRepos;
        private readonly IRepository<Data_BienDong, long> _data_BienDongRepos;
        IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;

        private readonly ICache mainCache;

        public BaoCaoAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<KyThongKeKiemKe, long> dmKyThongKeKiemKeRepos,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<Bieu01TKKK_Huyen, long> bieu01TKKK_HuyenRepos,
            IRepository<Bieu01TKKK_Xa, long> bieu01TKKK_XaRepos,
            IRepository<Bieu02TKKK_Xa, long> bieu02TKKK_XaRepos,
            IRepository<Bieu04TKKK_Xa, long> bieu04TKKK_XaRepos,
            IRepository<Bieu05TKKK_Xa, long> bieu05TKKK_XaRepos,
            IRepository<Bieu01aKKNLT_Xa, long> bieu01aKKNLT_XaRepos,
            IRepository<Bieu01bKKNLT_Xa, long> bieu01bKKNLT_XaRepos,
            IRepository<Bieu01cKKNLT_Xa, long> bieu01cKKNLT_XaRepos,
            IRepository<Bieu01KKSL_Xa, long> bieu01KKSL_XaRepos,
            IRepository<Bieu02KKSL_Xa, long> bieu02KKSL_XaRepos,
            IRepository<BieuPhuLucIII, long> bieuPhuLucIIIRepos,
            IRepository<BieuPhuLucIV, long> bieuPhuLucIVRepos,
            IRepository<KhoanhDat_KyTruoc, long> khoanhDat_KyTruocRepos,
            IRepository<Data_Target, long> data_TargetRepos,
            IRepository<SoLieuKyTruoc, long> soLieuKyTruocRepos,
            IRepository<Data_Commune, long> data_CommuneRepos,
            IRepository<Data_TangGiamKhac, long> data_TangGiamKhacRepos,
            IRepository<Data_BienDong, long> data_BienDongRepos,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _dvhcRepos = dvhcRepos;
            _dmKyThongKeKiemKeRepos = dmKyThongKeKiemKeRepos;
            _bieu01TKKK_HuyenRepos = bieu01TKKK_HuyenRepos;
            _bieu01TKKK_XaRepos = bieu01TKKK_XaRepos;
            _bieu02TKKK_XaRepos = bieu02TKKK_XaRepos;
            _bieu04TKKK_XaRepos = bieu04TKKK_XaRepos;
            _bieu05TKKK_XaRepos = bieu05TKKK_XaRepos;
            _bieu01cKKNLT_XaRepos = bieu01cKKNLT_XaRepos;
            _bieu01bKKNLT_XaRepos = bieu01bKKNLT_XaRepos;
            _bieu01aKKNLT_XaRepos = bieu01aKKNLT_XaRepos;
            _bieu01KKSL_XaRepos = bieu01KKSL_XaRepos;
            _bieu02KKSL_XaRepos = bieu02KKSL_XaRepos;
            _bieuPhuLucIIIRepos = bieuPhuLucIIIRepos;
            _bieuPhuLucIVRepos = bieuPhuLucIVRepos;
            _khoanhDat_KyTruocRepos = khoanhDat_KyTruocRepos;
            _data_TargetRepos = data_TargetRepos;
            _soLieuKyTruocRepos = soLieuKyTruocRepos;
            _data_CommuneRepos = data_CommuneRepos;
            _data_TangGiamKhacRepos = data_TangGiamKhacRepos;
            _data_BienDongRepos = data_BienDongRepos;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
        }


        [AbpAuthorize(PermissionNames.Pages_Report_NopBaoCao)]
        public async Task<CommonResponseDto> NopBaoCao(long year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(x=>x.Ma == currentUser.DonViHanhChinhCode && x.Year == year);

                    if (objdata != null)
                    {
                        if (objdata.SoDVHCDaDuyet < objdata.SoDVHCCon && objdata.CapDVHCId != 4)
                        {
                            commonResponseDto.Message = "Chưa duyệt hết các ĐVHC trực thuộc";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }

                        objdata.NgayGui = DateTime.Now;
                        objdata.TrangThaiDuyet = (int)TRANG_THAI_DUYET.CHO_DUYET;

                        await _dvhcRepos.UpdateAsync(objdata);
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }

        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> ThongKeSoLieu()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var _thongke = new ThongKeSoLieuOutputDto();
                    var year = 2024;
                    _thongke.TongSoTinh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.Year == year);
                    _thongke.TongSoTinhHoanThanh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.Year == year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.TongSoHuyen = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.HUYEN && x.Year == year);
                    _thongke.TongSoHuyenHoanThanh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.HUYEN && x.Year == year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.TongSoXa = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.Year == year);
                    _thongke.TongSoXaHoanThanh = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.Year == year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    
                    var _listMaTinhMNPB = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_MIEN_NUI_PHIA_BAC).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDBSH = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_BANG_SONG_HONG).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDHMT = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DUYEN_HAI_MIEN_TRUNG).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhTN = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_TAY_NGUYEN).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDNBc = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_NAM_BO).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    var _listMaTinhDBSCL = await _dvhcRepos.GetAll().Where(x => x.CapDVHCId == (int)CAP_DVHC.TINH && x.MaVung == ((int)VUNG_MIEN.VUNG_DONG_BANG_SONG_CUU_LONG).ToString() && x.Year == year).Select(x => x.Ma).ToListAsync();
                    _thongke.VungMienNuiPhiaBac = await _dvhcRepos.CountAsync(x => _listMaTinhMNPB.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDongBangSongHong = await _dvhcRepos.CountAsync(x => _listMaTinhDBSH.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDuyenHaiMienTrung = await _dvhcRepos.CountAsync(x => _listMaTinhDHMT.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungTayNguyen = await _dvhcRepos.CountAsync(x => _listMaTinhTN.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDongNamBo = await _dvhcRepos.CountAsync(x => _listMaTinhDNBc.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    _thongke.VungDongBangSongCuuLong = await _dvhcRepos.CountAsync(x => _listMaTinhDBSCL.Contains(x.MaTinh) && x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                    
                    _thongke.PhanTramVungMienNuiPhiaBac = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungMienNuiPhiaBac / _thongke.TongSoXa.Value * 100, 2) : 0;
                    _thongke.PhanTramVungDongBangSongHong = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDongBangSongHong / _thongke.TongSoXa.Value * 100, 2) : 0;
                    _thongke.PhanTramVungDuyenHaiMienTrung = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDuyenHaiMienTrung / _thongke.TongSoXa.Value * 100, 2) : 0;
                    _thongke.PhanTramVungTayNguyen = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungTayNguyen / _thongke.TongSoXa.Value * 100, 2) : 0;
                    _thongke.PhanTramVungDongNamBo = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDongNamBo / _thongke.TongSoXa.Value * 100, 2) : 0;
                    _thongke.PhanTramVungDongBangSongCuuLong = _thongke.TongSoXa > 0 ? Math.Round((decimal)_thongke.VungDongBangSongCuuLong / _thongke.TongSoXa.Value * 100, 2) : 0;
                    _thongke.ChuaNopBaoCao = 100 - (_thongke.PhanTramVungMienNuiPhiaBac + _thongke.PhanTramVungDongBangSongHong + _thongke.PhanTramVungDuyenHaiMienTrung + _thongke.PhanTramVungTayNguyen + _thongke.PhanTramVungDongNamBo + _thongke.PhanTramVungDongBangSongCuuLong);
                    commonResponseDto.ReturnValue = _thongke;
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> DeleteAllDataXa(long year, string ma)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma && x.Year == year);

                    if (objdata != null)
                    {
                        if (objdata.TrangThaiDuyet != (int)TRANG_THAI_DUYET.CHUA_GUI)
                        {
                            commonResponseDto.Message = "ĐVHC này đang chờ duyệt hoặc đã duyệt, không thể xoá dữ liệu";
                            commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                            return commonResponseDto;
                        }

                        else
                        {
                            //xoá bieu01
                            var lstBieu01 = await _bieu01TKKK_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu01 != null && lstBieu01.Count > 0)
                            {
                                foreach (var item in lstBieu01)
                                {
                                    await _bieu01TKKK_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá bieu02
                            var lstBieu02 = await _bieu02TKKK_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu02 != null && lstBieu02.Count > 0)
                            {
                                foreach (var item in lstBieu02)
                                {
                                    await _bieu02TKKK_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá bieu04
                            var lstBieu04 = await _bieu04TKKK_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu04 != null && lstBieu04.Count > 0)
                            {
                                foreach (var item in lstBieu04)
                                {
                                    await _bieu04TKKK_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá bieu05
                            var lstBieu05 = await _bieu05TKKK_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu05 != null && lstBieu05.Count > 0)
                            {
                                foreach (var item in lstBieu05)
                                {
                                    await _bieu05TKKK_XaRepos.DeleteAsync(item);
                                }
                            }
                            //xoá Bieu01aKKNLT_Xa
                            var lstBieu01aKKNLT_Xa = await _bieu01aKKNLT_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu01aKKNLT_Xa != null && lstBieu01aKKNLT_Xa.Count > 0)
                            {
                                foreach (var item in lstBieu01aKKNLT_Xa)
                                {
                                    await _bieu01aKKNLT_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá Bieu01bKKNLT
                            var lstBieu01bKKNLT = await _bieu01bKKNLT_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu01bKKNLT != null && lstBieu01bKKNLT.Count > 0)
                            {
                                foreach (var item in lstBieu01bKKNLT)
                                {
                                    await _bieu01bKKNLT_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá Bieu01cKKNLT
                            var lstBieu01cKKNLT = await _bieu01cKKNLT_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu01cKKNLT != null && lstBieu01cKKNLT.Count > 0)
                            {
                                foreach (var item in lstBieu01cKKNLT)
                                {
                                    await _bieu01cKKNLT_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá bieu01KKSL_Xa
                            var lstBieu01KKSL_Xa = await _bieu01KKSL_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu01KKSL_Xa != null && lstBieu01KKSL_Xa.Count > 0)
                            {
                                foreach (var item in lstBieu01KKSL_Xa)
                                {
                                    await _bieu01KKSL_XaRepos.DeleteAsync(item);
                                }
                            }
                            //xoá bieu02KKSL_Xa
                            var lstBieu02KKSL_Xa = await _bieu02KKSL_XaRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieu02KKSL_Xa != null && lstBieu02KKSL_Xa.Count > 0)
                            {
                                foreach (var item in lstBieu02KKSL_Xa)
                                {
                                    await _bieu02KKSL_XaRepos.DeleteAsync(item);
                                }
                            }

                            //xoá bieuPhuLucIII
                            var lstBieuPhuLucIII = await _bieuPhuLucIIIRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieuPhuLucIII != null && lstBieuPhuLucIII.Count > 0)
                            {
                                foreach (var item in lstBieuPhuLucIII)
                                {
                                    await _bieuPhuLucIIIRepos.DeleteAsync(item);
                                }
                            }

                            //xoá BieuPhuLucIV
                            var lstBieuPhuLucIV = await _bieuPhuLucIVRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstBieuPhuLucIV != null && lstBieuPhuLucIV.Count > 0)
                            {
                                foreach (var item in lstBieuPhuLucIV)
                                {
                                    await _bieuPhuLucIVRepos.DeleteAsync(item);
                                }
                            }

                            //xoá KhoanhDat_KyTruoc
                            var lstKhoanhDat_KyTruoc = await _khoanhDat_KyTruocRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstKhoanhDat_KyTruoc != null && lstKhoanhDat_KyTruoc.Count > 0)
                            {
                                foreach (var item in lstKhoanhDat_KyTruoc)
                                {
                                    await _khoanhDat_KyTruocRepos.DeleteAsync(item);
                                }
                            }

                            //xoá Data_Target
                            var lstData_Target = await _data_TargetRepos.GetAll().Where(x => x.MaDVHCCapXa == ma && x.year == year).ToListAsync();
                            if (lstData_Target != null && lstData_Target.Count > 0)
                            {
                                foreach (var item in lstData_Target)
                                {
                                    await _data_TargetRepos.DeleteAsync(item);
                                }
                            }

                            //xoá Data_Commune
                            var lstData_Commune = await _data_CommuneRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstData_Commune != null && lstData_Commune.Count > 0)
                            {
                                foreach (var item in lstData_Commune)
                                {
                                    await _data_CommuneRepos.DeleteAsync(item);
                                }
                            }

                            //xoá Data_TangGiamKhac
                            var lstData_TangGiamKhac = await _data_TangGiamKhacRepos.GetAll().Where(x => x.MaDVHCCapXa == ma && x.Year == year).ToListAsync();
                            if (lstData_TangGiamKhac != null && lstData_TangGiamKhac.Count > 0)
                            {
                                foreach (var item in lstData_TangGiamKhac)
                                {
                                    await _data_TangGiamKhacRepos.DeleteAsync(item);
                                }
                            }

                            //xoá SoLieuKyTruoc
                            var lstSoLieuKyTruoc = await _soLieuKyTruocRepos.GetAll().Where(x => x.MaXa == ma && x.Year == year).ToListAsync();
                            if (lstSoLieuKyTruoc != null && lstSoLieuKyTruoc.Count > 0)
                            {
                                foreach (var item in lstSoLieuKyTruoc)
                                {
                                    await _soLieuKyTruocRepos.DeleteAsync(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        commonResponseDto.Message = "ĐVHC này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                    uow.Complete();
                }
                catch (Exception ex)
                {
                    uow.Dispose();
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                    Logger.Fatal(ex.Message);
                }
            }
            return commonResponseDto;
        }
    }
}

﻿using Abp.Application.Services.Dto;
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
using System.Transactions;
using KiemKeDatDai.AppCore.Utility;

namespace KiemKeDatDai.App.DanhMucDVHC
{
    [AbpAuthorize]
    public class DanhMucDVHCAppService : KiemKeDatDaiAppServiceBase, IDonViHanhChinhAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
        private readonly IRepository<CapDVHC, long> _cdvhcRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public DanhMucDVHCAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<DonViHanhChinh, long> dvhcRepos,
            IRepository<CapDVHC, long> cdvhcRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor
            //ILogAppService iLogAppService
            )
        {
            _cacheManager = cacheManager;
            _iocResolver = iocResolver;
            _dvhcRepos = dvhcRepos;
            _cdvhcRepos = cdvhcRepos;
            _userRepos = userRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            //_iLogAppService = iLogAppService;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetByUser(DVHCInput input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstDVHC = new List<DVHCOutputDto>();
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var userObj = await _userRepos.FirstOrDefaultAsync(input.UserId.Value);
                if (userObj != null)
                {
                    var dvhcId = userObj.DonViHanhChinhId;
                    if (dvhcId != 0)
                    {
                        var query = (from dvhc in _dvhcRepos.GetAll()
                                     join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.Id
                                     where dvhc.Id == dvhcId && dvhc.Year == input.Year
                                     select new DVHCOutputDto
                                     {
                                         Id = dvhc.Id,
                                         TenVung = dvhc.TenVung,
                                         MaVung = dvhc.MaVung,
                                         TenTinh = dvhc.TenTinh,
                                         MaTinh = dvhc.MaTinh,
                                         TenHuyen = dvhc.TenHuyen,
                                         MaHuyen = dvhc.MaHuyen,
                                         TenXa = dvhc.TenXa,
                                         MaXa = dvhc.MaXa,
                                         Ma = dvhc.Ma,
                                         Name = dvhc.Name,
                                         Parent_id = dvhc.Parent_id,
                                         CapDVHCId = dvhc.CapDVHCId,
                                         Active = dvhc.Active,
                                         Year = dvhc.Year,
                                         TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                         ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1
                                     });
                        lstDVHC = await query.ToListAsync();
                        commonResponseDto.ReturnValue = lstDVHC;
                        commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                        commonResponseDto.Message = "Thành Công";
                    }
                    else
                    {
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        commonResponseDto.Message = "Không tìm thấy tài khoản người dùng trong hệ thống!";
                    }
                }

            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstDVHC = new List<DVHCOutputDto>();
                PagedResultDto<DVHCDto> pagedResultDto = new PagedResultDto<DVHCDto>();
                var query = (from dvhc in _dvhcRepos.GetAll()
                             join cdvhc in _cdvhcRepos.GetAll() on dvhc.CapDVHCId equals cdvhc.Id
                             where dvhc.Parent_id == id
                             select new DVHCOutputDto
                             {
                                 Id = dvhc.Id,
                                 TenTinh = dvhc.TenTinh,
                                 MaTinh = dvhc.MaTinh,
                                 TenHuyen = dvhc.TenHuyen,
                                 MaHuyen = dvhc.MaHuyen,
                                 TenXa = dvhc.TenXa,
                                 MaXa = dvhc.MaXa,
                                 Ma = dvhc.Ma,
                                 Name = dvhc.Name,
                                 Parent_id = dvhc.Parent_id,
                                 CapDVHCId = dvhc.CapDVHCId,
                                 Active = dvhc.Active,
                                 Year = dvhc.Year,
                                 TrangThaiDuyet = dvhc.TrangThaiDuyet,
                                 ChildStatus = cdvhc.CapDVHCMin == true ? 0 : 1
                             });
                if (query != null)
                {
                    lstDVHC = await query.ToListAsync();
                    commonResponseDto.ReturnValue = lstDVHC;
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không có dữ liệu cấp dưới!";
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> CreateOrUpdate(DVHCInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (input.Id != 0)
                {
                    var data = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == input.Id);
                    if (data != null)
                    {
                        data.TenTinh = input.TenTinh;
                        data.MaTinh = input.MaTinh;
                        data.TenHuyen = input.TenHuyen;
                        data.MaHuyen = input.MaHuyen;
                        data.TenXa = input.TenXa;
                        data.MaXa = input.MaXa;
                        data.Name = input.Name;
                        data.Parent_id = input.Parent_id;
                        data.CapDVHCId = input.CapDVHCId;
                        data.Active = input.Active;
                        data.Year = input.Year;
                        data.TrangThaiDuyet = input.TrangThaiDuyet;
                        await _dvhcRepos.UpdateAsync(data);
                    }
                    else
                    {
                        commonResponseDto.Message = "Đơn vị hành chính này không tồn tại";
                        commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                        return commonResponseDto;
                    }
                }
                else
                {
                    var dvhc = input.MapTo<DVHCInputDto>();
                    await _dvhcRepos.InsertAsync(dvhc);
                }
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var objDVHC = await _dvhcRepos.FirstOrDefaultAsync(x => x.Id == id);
                if (objDVHC != null)
                {
                    await _dvhcRepos.DeleteAsync(objDVHC);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Dơn vị hành chính này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> BaoCaoDVHC(BaoCaoInPutDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            try
            {
                var currentUser = await GetCurrentUserAsync();
                var lstBaoCao = new List<BaoCaoDonViHanhChinhOutPutDto>();
                var objdata = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == input.Ma && x.Year == input.Year);
                if (objdata != null)
                {
                    var baoCaoDVHC = new BaoCaoDonViHanhChinhOutPutDto
                    {
                        Id = objdata.Id,
                        Ten = objdata.Name,
                        MaDVHC = objdata.Ma,
                        ParentId = objdata.Parent_id,
                        NgayCapNhat = objdata.NgayGui,
                        TrangThaiDuyet = objdata.TrangThaiDuyet,
                        ChildStatus = 1
                    };
                    baoCaoDVHC.Root = currentUser.DonViHanhChinhCode == input.Ma ? true : false;
                    switch (objdata.CapDVHCId)
                    {
                        case (int)CAP_DVHC.TRUNG_UONG:
                            baoCaoDVHC.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA);
                            baoCaoDVHC.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.VUNG:
                            var lstMaTinh = await _dvhcRepos.GetAll().Where(x => x.Parent_Code == objdata.Ma).Select(x => x.Ma).ToArrayAsync();
                            baoCaoDVHC.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh));
                            baoCaoDVHC.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.TINH:
                            baoCaoDVHC.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == objdata.MaTinh);
                            baoCaoDVHC.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == objdata.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == objdata.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.HUYEN:
                            baoCaoDVHC.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == objdata.MaHuyen);
                            baoCaoDVHC.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == objdata.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                            baoCaoDVHC.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == objdata.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                            break;
                        case (int)CAP_DVHC.XA:
                            baoCaoDVHC.Tong = 1;
                            baoCaoDVHC.TongDuyet = objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET ? 1 : 0;
                            baoCaoDVHC.TongNop = objdata.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET ? 1 : 0;
                            baoCaoDVHC.ChildStatus = 0;
                            break;
                        default:
                            break;
                    }

                    var lstChild = await _dvhcRepos.GetAllListAsync(x => x.Parent_Code == input.Ma && x.Year == input.Year);
                    //Xác định  trạng thái button nộp báo cáo
                    if (baoCaoDVHC.Root == true)
                    {
                        baoCaoDVHC.IsNopBaoCao = (baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.CHO_DUYET && baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET) ? true : false;

                        //----------Tạm bỏ điều kiện check nộp
                        //if (baoCaoDVHC.ChildStatus == 0)
                        //{
                        //    baoCaoDVHC.IsNopBaoCao = baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET ? true : false;
                        //}
                        //else
                        //{
                        //    var soDaDuyet = await _dvhcRepos.CountAsync(x => x.Parent_Code == input.Ma && x.Year == input.Year && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                        //    if (soDaDuyet == lstChild.Count && baoCaoDVHC.TrangThaiDuyet != (int)TRANG_THAI_DUYET.DA_DUYET)
                        //        baoCaoDVHC.IsNopBaoCao = true;
                        //    else
                        //        baoCaoDVHC.IsNopBaoCao = false;
                        //}
                    }
                    lstBaoCao.Add(baoCaoDVHC);
                    if (lstChild != null)
                    {
                        foreach (var item in lstChild)
                        {

                            var baoCaoDVHC_child = new BaoCaoDonViHanhChinhOutPutDto
                            {
                                Id = item.Id,
                                Ten = item.Name,
                                MaDVHC = item.Ma,
                                ParentId = item.Parent_id,
                                NgayCapNhat = item.NgayGui,
                                TrangThaiDuyet = item.TrangThaiDuyet,
                                IsNopBaoCao = false
                            };
                            switch (item.CapDVHCId)
                            {
                                case (int)CAP_DVHC.VUNG:
                                    var lstMaTinh = await _dvhcRepos.GetAll().Where(x => x.Parent_Code == objdata.Ma).Select(x => x.Ma).ToArrayAsync();
                                    baoCaoDVHC_child.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh));
                                    baoCaoDVHC_child.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                                    baoCaoDVHC_child.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && lstMaTinh.Contains(x.MaTinh) && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                                    baoCaoDVHC_child.ChildStatus = 1;
                                    break;
                                case (int)CAP_DVHC.TINH:
                                    baoCaoDVHC_child.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == item.MaTinh);
                                    baoCaoDVHC_child.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == item.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                                    baoCaoDVHC_child.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaTinh == item.MaTinh && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                                    baoCaoDVHC_child.ChildStatus = 1;
                                    break;
                                case (int)CAP_DVHC.HUYEN:
                                    baoCaoDVHC_child.Tong = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == item.MaHuyen);
                                    baoCaoDVHC_child.TongDuyet = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == item.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET);
                                    baoCaoDVHC_child.TongNop = await _dvhcRepos.CountAsync(x => x.CapDVHCId == (int)CAP_DVHC.XA && x.MaHuyen == item.MaHuyen && x.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET);
                                    baoCaoDVHC_child.ChildStatus = 1;
                                    break;
                                case (int)CAP_DVHC.XA:
                                    baoCaoDVHC_child.Tong = 1;
                                    baoCaoDVHC_child.TongDuyet = item.TrangThaiDuyet == (int)TRANG_THAI_DUYET.DA_DUYET ? 1 : 0;
                                    baoCaoDVHC_child.TongNop = item.TrangThaiDuyet == (int)TRANG_THAI_DUYET.CHO_DUYET ? 1 : 0;
                                    baoCaoDVHC_child.ChildStatus = 0;
                                    break;
                                default:
                                    break;
                            }
                            lstBaoCao.Add(baoCaoDVHC_child);
                        }
                    }
                    commonResponseDto.ReturnValue = lstBaoCao;
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "ĐVHC này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Fatal(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetDropDownVung()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll() 
                             where dvhc.CapDVHCId == (int)CAP_DVHC.VUNG
                             select new DropDownListDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetDropDownTinh()
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll() 
                             where dvhc.CapDVHCId == (int)CAP_DVHC.TINH
                             select new DropDownListDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetDropDownHuyenByTinhId(long tinhId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll() 
                             where dvhc.CapDVHCId == (int)CAP_DVHC.HUYEN && dvhc.Parent_id == tinhId
                             select new DropDownListDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetDropDownXaByHuyenId(long huyenId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from dvhc in _dvhcRepos.GetAll() 
                             where dvhc.CapDVHCId == (int)CAP_DVHC.XA && dvhc.Parent_id == huyenId
                             select new DropDownListDto
                             {
                                 Id = dvhc.Id,
                                 Name = dvhc.Name,
                             });
                commonResponseDto.ReturnValue = await query.ToListAsync();
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }
    }
}

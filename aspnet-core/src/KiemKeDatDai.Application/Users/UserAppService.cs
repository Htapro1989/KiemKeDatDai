using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Authorization;
using KiemKeDatDai.Authorization.Roles;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.Roles.Dto;
using KiemKeDatDai.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static KiemKeDatDai.CommonEnum;
using Microsoft.Extensions.Caching.Memory;
using KiemKeDatDai.Sessions;
using Abp.Domain.Uow;
using System.Transactions;
using Aspose.Cells;

namespace KiemKeDatDai.RisApplication;

[AbpAuthorize]
public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
{
    private readonly UserManager _userManager;
    private readonly RoleManager _roleManager;
    private readonly IRepository<Authorization.Roles.Role> _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAbpSession _abpSession;
    private readonly LogInManager _logInManager;
    private readonly IRepository<DonViHanhChinh, long> _dvhcRepos;
    private readonly IRepository<User, long> _userRepos;
    private readonly IMemoryCache _cache;
    IUnitOfWorkManager _unitOfWorkManager;

    public UserAppService(
        IRepository<User, long> repository,
        UserManager userManager,
        RoleManager roleManager,
        IRepository<Authorization.Roles.Role> roleRepository,
        IPasswordHasher<User> passwordHasher,
        IAbpSession abpSession,
        IRepository<DonViHanhChinh, long> dvhcRepos,
        IRepository<User, long> userRepos,
        IMemoryCache cache,
        IUnitOfWorkManager unitOfWorkManager,
        LogInManager logInManager)
        : base(repository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _abpSession = abpSession;
        _logInManager = logInManager;
        _dvhcRepos = dvhcRepos;
        _userRepos = userRepos;
        _cache = cache;
        _unitOfWorkManager = unitOfWorkManager;
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public override async Task<UserDto> CreateAsync(CreateUserDto input)
    {
        CheckCreatePermission();

        var user = ObjectMapper.Map<User>(input);

        user.TenantId = AbpSession.TenantId;
        user.IsEmailConfirmed = true;
        user.IsChangePass = true;

        await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

        CheckErrors(await _userManager.CreateAsync(user, input.Password));

        if (input.RoleNames != null)
        {
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
        }

        CurrentUnitOfWork.SaveChanges();

        return MapToEntityDto(user);
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public override async Task<UserDto> UpdateAsync(UserDto input)
    {
        CheckUpdatePermission();

        var user = await _userManager.GetUserByIdAsync(input.Id);

        MapToEntity(input, user);

        CheckErrors(await _userManager.UpdateAsync(user));

        if (input.RoleNames != null)
        {
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
        }

        return await GetAsync(input);
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public override async Task DeleteAsync(EntityDto<long> input)
    {
        var user = await _userManager.GetUserByIdAsync(input.Id);
        await _userManager.DeleteAsync(user);
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public async Task Activate(EntityDto<long> user)
    {
        await Repository.UpdateAsync(user.Id, async (entity) =>
        {
            entity.IsActive = true;
        });
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public async Task DeActivate(EntityDto<long> user)
    {
        await Repository.UpdateAsync(user.Id, async (entity) =>
        {
            entity.IsActive = false;
        });
    }

    public async Task<ListResultDto<RoleDto>> GetRoles()
    {
        var roles = await _roleRepository.GetAllListAsync();
        return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
    }

    public async Task ChangeLanguage(ChangeUserLanguageDto input)
    {
        await SettingManager.ChangeSettingForUserAsync(
            AbpSession.ToUserIdentifier(),
            LocalizationSettingNames.DefaultLanguage,
            input.LanguageName
        );
    }

    protected override User MapToEntity(CreateUserDto createInput)
    {
        var user = ObjectMapper.Map<User>(createInput);
        user.SetNormalizedNames();
        return user;
    }

    protected override void MapToEntity(UserDto input, User user)
    {
        ObjectMapper.Map(input, user);
        user.SetNormalizedNames();
    }

    protected override UserDto MapToEntityDto(User user)
    {
        var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

        var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

        var userDto = base.MapToEntityDto(user);
        userDto.RoleNames = roles.ToArray();

        return userDto;
    }

    protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
    {
        return Repository.GetAllIncluding(x => x.Roles)
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
            .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
    }

    protected override async Task<User> GetEntityByIdAsync(long id)
    {
        var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            throw new EntityNotFoundException(typeof(User), id);
        }

        return user;
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
    {
        return query.OrderBy(input.Sorting);
    }

    protected virtual void CheckErrors(IdentityResult identityResult)
    {
        identityResult.CheckErrors(LocalizationManager);
    }

    public async Task<bool> ChangePassword(ChangePasswordDto input)
    {
        await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

        var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
        if (user == null)
        {
            throw new Exception("There is no current user!");
        }

        if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
        {
            CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            user.IsChangePass = false;
            CheckErrors(await _userManager.UpdateAsync(user));
        }
        else
        {
            CheckErrors(IdentityResult.Failed(new IdentityError
            {
                Description = "Incorrect password."
            }));
        }

        return true;
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public async Task<bool> ResetPassword(ResetPasswordDto input)
    {
        if (_abpSession.UserId == null)
        {
            throw new UserFriendlyException("Please log in before attempting to reset password.");
        }

        var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
        //var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
        //if (loginAsync.Result != AbpLoginResultType.Success)
        //{
        //    throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
        //}

        if (currentUser.IsDeleted || !currentUser.IsActive)
        {
            return false;
        }

        var roles = await _userManager.GetRolesAsync(currentUser);
        if (!roles.Contains(StaticRoleNames.Tenants.Admin))
        {
            throw new UserFriendlyException("Only administrators may reset passwords.");
        }

        var user = await _userManager.GetUserByIdAsync(input.UserId);
        if (user != null)
        {
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        return true;
    }

    [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
    public async Task<CommonResponseDto> GetAllUser(PagedUserResultRequestDto input)
    {
        CommonResponseDto commonResponseDto = new CommonResponseDto();
        try
        {
            var query = (from obj in _userRepos.GetAll()
                         select new UserDto
                         {
                             Id = obj.Id,
                             UserName = obj.UserName,
                             Name = obj.Name,
                             Surname = obj.Surname,
                             FullName = obj.FullName,
                             EmailAddress = obj.EmailAddress,
                             IsActive = obj.IsActive,
                             CreationTime = obj.CreationTime,
                             DonViHanhChinhId = obj.DonViHanhChinhId,
                             DonViHanhChinhCode = obj.DonViHanhChinhCode
                         })
                         .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.ToLower().Contains(input.Keyword.ToLower())
                         || x.FullName.ToLower().Contains(input.Keyword.ToLower())
                         || x.UserName.ToLower().Contains(input.Keyword.ToLower())
                         || x.Name.ToLower().Contains(input.Keyword.ToLower())
                         || x.EmailAddress.ToLower().Contains(input.Keyword.ToLower()))
                         .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);

            var totalCount = await query.CountAsync();
            var lstUser = await query.OrderBy(x => x.CreationTime)
                                .Skip(input.SkipCount)
                                .Take(input.MaxResultCount)
                                .ToListAsync();

            if (lstUser.Count > 0)
            {
                foreach (var item in lstUser)
                {
                    item.DonViHanhChinh = _dvhcRepos.Single(x => x.Ma == item.DonViHanhChinhCode).Name;
                }
            }
            commonResponseDto.ReturnValue = new PagedResultDto<UserDto>()
            {
                Items = lstUser,
                TotalCount = totalCount
            };
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

    public async Task<CommonResponseDto> GetUserByMaDVHC(PagedUserResultRequestDto input)
    {
        CommonResponseDto commonResponseDto = new CommonResponseDto();
        try
        {
            if (string.IsNullOrEmpty(input.Ma))
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = "Mã đơn vị hành chính không được để trống.";
                return commonResponseDto;
            }
            var lstMa = await GetChildrenMa(input.Ma);
            lstMa.Add(input.Ma);

            var query = (from obj in _userRepos.GetAll()
                         join dvhc in _dvhcRepos.GetAll() on obj.DonViHanhChinhCode equals dvhc.Ma
                         where lstMa.Contains(dvhc.Ma)
                         select new UserDto
                         {
                             Id = obj.Id,
                             UserName = obj.UserName,
                             Name = obj.Name,
                             Surname = obj.Surname,
                             FullName = obj.FullName,
                             EmailAddress = obj.EmailAddress,
                             IsActive = obj.IsActive,
                             CreationTime = obj.CreationTime,
                             DonViHanhChinhId = obj.DonViHanhChinhId,
                             DonViHanhChinhCode = obj.DonViHanhChinhCode,
                             DonViHanhChinh = dvhc.Name
                         });
            var totalCount = await query.CountAsync();
            var lstUser = await query.OrderBy(x => x.CreationTime)
                                .Skip(input.SkipCount)
                                .Take(input.MaxResultCount)
                                .ToListAsync();

            commonResponseDto.ReturnValue = new PagedResultDto<UserDto>()
            {
                Items = lstUser,
                TotalCount = totalCount
            };
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

    public async Task<List<string>> GetChildrenMa(string ma)
    {
        var lstMa = new List<string>();
        var dvhc = await _dvhcRepos.FirstOrDefaultAsync(x => x.Ma == ma);
        var allDvhc = await _dvhcRepos.GetAll()
                                      .Where(x => !string.IsNullOrWhiteSpace(x.Ma))
                                      .ToListAsync();
        var lstMaVung = new List<string>();
        var lstMaTinh = new List<string>();

        switch (dvhc.CapDVHCId)
        {
            case (int)CAP_DVHC.TRUNG_UONG:
                {
                    lstMaVung = allDvhc.Where(x => x.Parent_Code == ma).Select(x => x.Ma).ToList();
                    if (lstMaVung.Count > 0)
                    {
                        lstMa.AddRange(lstMaVung);
                        foreach (var maVung in lstMaVung)
                        {
                            lstMaTinh.AddRange(allDvhc.Where(x => x.Parent_Code == maVung).Select(x => x.Ma).ToList());
                        }
                        if (lstMaTinh.Count > 0)
                        {
                            foreach (var maTinh in lstMaTinh)
                            {
                                lstMa.AddRange(allDvhc.Where(x => x.Parent_Code == maTinh).Select(x => x.Ma).ToList());
                            }
                        }
                    }
                    break;
                }
            case (int)CAP_DVHC.VUNG:
                lstMaTinh = allDvhc.Where(x => x.Parent_Code == ma).Select(x => x.Ma).ToList();
                if (lstMaTinh.Count > 0)
                {
                    foreach (var maTinh in lstMaTinh)
                    {
                        lstMa.AddRange(allDvhc.Where(x => x.MaTinh == maTinh).Select(x => x.Ma).ToList());
                    }
                }
                break;
            case (int)CAP_DVHC.TINH:
                lstMa.AddRange(allDvhc.Where(x => x.MaTinh == ma).Select(x => x.Ma).ToList());
                break;
            case (int)CAP_DVHC.HUYEN:
                lstMa.AddRange(allDvhc.Where(x => x.MaHuyen == ma).Select(x => x.Ma).ToList());
                break;
            case (int)CAP_DVHC.XA:
                break;
        }

        return lstMa;
    }

    private async Task<List<DonViHanhChinh>> GetAllDVHCWithCacheAsync()
    {
        return await _cache.GetOrCreateAsync("AllDVHCs", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            return await _dvhcRepos.GetAll()
                .Select(x => new DonViHanhChinh
                {
                    Id = x.Id,
                    Name = x.Name,
                    TenVung = x.TenVung,
                    MaVung = x.MaVung,
                    TenTinh = x.TenTinh,
                    MaTinh = x.MaTinh,
                    TenHuyen = x.TenHuyen,
                    MaHuyen = x.MaHuyen,
                    TenXa = x.TenXa,
                    MaXa = x.MaXa,
                    Ma = x.Ma,
                    Parent_id = x.Parent_id,
                    Parent_Code = x.Parent_Code,
                    CapDVHCId = x.CapDVHCId,
                    TrangThaiDuyet = x.TrangThaiDuyet,
                    Year = x.Year,
                    CreationTime = x.CreationTime,
                    Active = x.Active
                })
                .ToListAsync();
        });
    }

    public async Task<CommonResponseDto> UpdateByCapDvhc(int capDvhc, string[] role)
    {
        CommonResponseDto commonResponseDto = new CommonResponseDto();
        using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
        {
            try
            {
                var allDVHC = await _dvhcRepos.GetAllAsync();

                var query = from u in _userRepos.GetAll()
                            join dvhc in _dvhcRepos.GetAll() on u.DonViHanhChinhCode equals dvhc.Ma
                            where dvhc.CapDVHCId == capDvhc && u.IsActive == true
                            select new UserDto
                            {
                                Id = u.Id,
                                UserName = u.UserName,
                                Name = u.Name,
                                Surname = u.Surname,
                                FullName = u.FullName,
                                EmailAddress = u.EmailAddress,
                                IsActive = u.IsActive,
                                CreationTime = u.CreationTime,
                                DonViHanhChinhId = u.DonViHanhChinhId,
                                DonViHanhChinhCode = u.DonViHanhChinhCode,
                                RoleNames = role
                            };

                var lstUser = await query.ToListAsync();

                foreach (var item in lstUser)
                {
                    await UpdateAsync(item);
                }
                uow.Complete();

                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                uow.Dispose();
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
        }
        return commonResponseDto;
    }
}


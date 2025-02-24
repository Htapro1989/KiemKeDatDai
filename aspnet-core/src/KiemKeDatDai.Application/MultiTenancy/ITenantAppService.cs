using Abp.Application.Services;
using KiemKeDatDai.MultiTenancy.Dto;

namespace KiemKeDatDai.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}


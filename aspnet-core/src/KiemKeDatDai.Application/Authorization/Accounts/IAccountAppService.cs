using Abp.Application.Services;
using KiemKeDatDai.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace KiemKeDatDai.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}

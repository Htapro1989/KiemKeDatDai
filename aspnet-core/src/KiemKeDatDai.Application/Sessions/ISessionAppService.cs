using Abp.Application.Services;
using KiemKeDatDai.Sessions.Dto;
using System.Threading.Tasks;

namespace KiemKeDatDai.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}

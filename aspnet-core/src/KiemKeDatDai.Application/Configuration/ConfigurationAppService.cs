using Abp.Authorization;
using Abp.Runtime.Session;
using KiemKeDatDai.Configuration.Dto;
using System.Threading.Tasks;

namespace KiemKeDatDai.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : KiemKeDatDaiAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}

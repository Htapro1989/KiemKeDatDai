using KiemKeDatDai.Configuration.Dto;
using System.Threading.Tasks;

namespace KiemKeDatDai.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}

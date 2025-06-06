using Abp.Application.Services;
using Abp.Application.Services.Dto;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Roles.Dto;
using KiemKeDatDai.Users.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KiemKeDatDai.RisApplication;

public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
{
    Task DeActivate(EntityDto<long> user);
    Task Activate(EntityDto<long> user);
    Task<ListResultDto<RoleDto>> GetRoles();
    Task ChangeLanguage(ChangeUserLanguageDto input);

    Task<bool> ChangePassword(ChangePasswordDto input);
    Task<CommonResponseDto> GetAllUser(PagedUserResultRequestDto input);
    Task<CommonResponseDto> GetUserByMaDVHC(PagedUserResultRequestDto input);
    Task<CommonResponseDto> UpdateByCapDvhc(int capDvhc, string[] role);
    Task<List<string>> GetChildrenMa(string ma);
    Task<CommonResponseDto> UploadFileUser(IFormFile fileUpload);
}

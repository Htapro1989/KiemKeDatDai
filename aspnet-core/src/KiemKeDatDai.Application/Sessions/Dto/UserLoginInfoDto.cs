using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using KiemKeDatDai.Authorization.Users;

namespace KiemKeDatDai.Sessions.Dto;

[AutoMapFrom(typeof(User))]
public class UserLoginInfoDto : EntityDto<long>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string UserName { get; set; }

    public string EmailAddress { get; set; }
    public bool IsAdmin { get; set; }
    public string Role { get; set; }
    public string RoleDescription { get; set; }
    public string Message_Info { get; set; }
    public long? DonViHanhChinhId { get; set; }
    public string DonViHanhChinhCode { get; set; }
    public string DonViHanhChinh { get; set; }
}

using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace KiemKeDatDai.Authorization;

public class KiemKeDatDaiAuthorizationProvider : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("Pages"));

        var administration = pages.CreateChildPermission(PermissionNames.Pages_Administration, L("Administration"));

        var roles = administration.CreateChildPermission(PermissionNames.Pages_Administration_Roles, L("Roles"));
        roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Create, L("CreatingNewRole"));
        roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Edit, L("EditingRole"));
        roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Delete, L("DeletingRole"));

        var users = administration.CreateChildPermission(PermissionNames.Pages_Administration_Users, L("Users"));
        users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Create, L("CreatingNewUser"));
        users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Edit, L("EditingUser"));
        users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Delete, L("DeletingUser"));
        users.CreateChildPermission(PermissionNames.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
        users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

        var systemManager = administration.CreateChildPermission(PermissionNames.Pages_Administration_System, L("SystemManager"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_CapDvhc, L("CapDvhc"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_ConfigSystem, L("ConfigSystem"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_Dvhc, L("Dvhc"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_BieuMau, L("BieuMau"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_KyKiemKe, L("KyKiemKe"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_News, L("News"));
        systemManager.CreateChildPermission(PermissionNames.Pages_Administration_System_YKien, L("YKien"));

        var report = administration.CreateChildPermission(PermissionNames.Pages_Report, L("Report"));
        report.CreateChildPermission(PermissionNames.Pages_Report_UploadAPI, L("UploadAPI"));
        report.CreateChildPermission(PermissionNames.Pages_Report_Upload, L("Upload"));
        report.CreateChildPermission(PermissionNames.Pages_Report_DownloadBaoCao, L("DownloadBaoCao"));
        report.CreateChildPermission(PermissionNames.Pages_Report_NopBaoCao, L("NopBaoCao"));
        report.CreateChildPermission(PermissionNames.Pages_Report_XemBaoCao, L("XemBaoCao"));
        report.CreateChildPermission(PermissionNames.Pages_Report_DuyetBaoCao, L("DuyetBaoCao"));
        report.CreateChildPermission(PermissionNames.Pages_Report_HuyBaoCao, L("HuyBaoCao"));
        report.CreateChildPermission(PermissionNames.Pages_Report_DownloadFile, L("DownloadFile"));
        report.CreateChildPermission(PermissionNames.Pages_Report_NhapBieu, L("NhapBieu"));

        context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
        context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
        context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
        context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, KiemKeDatDaiConsts.LocalizationSourceName);
    }
}

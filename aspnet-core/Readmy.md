﻿
---------user/pass remote server-------
server chính
IP: 45.120.229.45
user: Administrator
pass: Nguyenthikimng@n@1978@kBYamaXW$333666
pass sql: KiemKeDatDai@2024$123A

server qa
IP: 45.120.229.43
user: Administrator
pass: Nguyenthikimng@n@1978@dU8NJw4E$2025

User admin: admin/ Letinh$202512368

link api: http://45.120.229.45:8000/swagger/index.html
link web: http://45.120.229.45:8003/


-------link hướng dẫn xoá WebDAV---------
https://dev.to/devvsamjuel/webdav-put-and-delete-http-verbs-remove-webdav-module-on-iis-3l1g

Khôi phục .net: dotnet restore

--------link con web cũ bên tổng cục----------
http://tk14.gdla.gov.vn/
user: host; pass: L@tinh2022


---------inser vai trò cấp xã---------
insert into AbpUserRoles(CreationTime, CreatorUserId, RoleId, UserId) 

select '2025-04-25 00:52:30.3751994', 1, 4, a.Id
from AbpUsers a
inner join DonViHanhChinh b on a.donvihanhchinhid = b.id
where b.capDVHCId = 4

-----------lấy danh sách các xã đã đẩy dữ liệu--------
select MaXa, TenXa, TenHuyen, TenTinh, NgayDuyet, NgayGui, TrangThaiDuyet 
from DonViHanhChinh dv
inner join [File] f on dv.MaXa = f.MaDVHC
where dv.CapDVHCId=4 and dv.Active=1 and dv.IsDeleted = 0 and dv.Year=2024 and f.IsDeleted = 0 and f.Year=2024 and f.FileType='FILE_KYTHONGKE'
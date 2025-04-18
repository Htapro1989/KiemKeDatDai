using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using KiemKeDatDai;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Abp.Threading;
using KiemKeDatDai.Sessions;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using KiemKeDatDai.RisApplication;
using static KiemKeDatDai.CommonEnum;
using System.IO;
using KiemKeDatDai.AppCore.Utility;

namespace KiemKeDatDai.App.DMBieuMau
{
    [AbpAuthorize]
    public class YKienAppService : KiemKeDatDaiAppServiceBase, IYKienAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<YKien, long> _yKienRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IRepository<EntitiesDb.File, long> _fileRepos;
        private readonly IFileKiemKeAppService _iFileKiemKeAppService;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;

        public YKienAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<YKien, long> yKienRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            IRepository<EntitiesDb.File, long> fileRepos,
            IFileKiemKeAppService iFileKiemKeAppService
            //ILogAppService iLogAppService
            )
        {
            _yKienRepos = yKienRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _fileRepos = fileRepos;
            _iFileKiemKeAppService = iFileKiemKeAppService;
            //_iLogAppService = iLogAppService;
        }
        public async Task<CommonResponseDto> GetAll(YKienDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = (from obj in _yKienRepos.GetAll()
                             select new YKienOuputDto
                             {
                                 Id = obj.Id,
                                 Name = obj.Name,
                                 Email = obj.Email,
                                 DonViCongTac = obj.DonViCongTac,
                                 NoiDungYKien = obj.NoiDungYKien,
                                 NoiDungTraLoi = obj.NoiDungTraLoi,
                                 Year = obj.Year,
                                 PheDuyet = obj.PheDuyet,
                                 FileId = obj.FileId,
                                 NgayTraLoi = obj.NgayTraLoi,
                                 CreationTime = obj.CreationTime,
                                 Active = obj.Active
                             })
                             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.ToLower().Contains(input.Filter.ToLower())
                             || x.Email.ToLower().Contains(input.Filter.ToLower())
                             || x.DonViCongTac.ToLower().Contains(input.Filter.ToLower())
                             || x.NoiDungYKien.ToLower().Contains(input.Filter.ToLower())
                             || x.NoiDungTraLoi.ToLower().Contains(input.Filter.ToLower())
                             || x.Name.ToLower().Contains(input.Filter.ToLower()));

                var totalCount = await query.CountAsync();
                var lstYKien = await query.OrderBy(x => x.CreationTime)
                                    .Skip(input.SkipCount)
                                    .Take(input.MaxResultCount)
                                    .ToListAsync();

                foreach ( var item in lstYKien)
                {
                    if (item.FileId != null)
                    {
                        item.FileName = _fileRepos.SingleAsync(x => x.Id == item.FileId).Result.FileName;
                    }
                }

                commonResponseDto.ReturnValue = new PagedResultDto<YKienOuputDto>()
                {
                    Items = lstYKien,
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
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                commonResponseDto.ReturnValue = await _yKienRepos.FirstOrDefaultAsync(id); 
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
        [AbpAllowAnonymous]
        public async Task<CommonResponseDto> CreateOrUpdate([FromForm] YKienInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (input.Id != 0)
                {
                    var data = await _yKienRepos.FirstOrDefaultAsync(input.Id);

                    if (data != null)
                    {
                        input.MapTo(data); long insertedFileID = 0;

                        if (input.File != null && input.File.Length > 0)
                        {
                            //delete old file
                            if (data.FileId != null)
                            {
                                var oldFile = await _fileRepos.FirstOrDefaultAsync(data.FileId.Value);
                                if (oldFile != null)
                                {
                                    System.IO.File.Delete(oldFile.FilePath);
                                    await _fileRepos.DeleteAsync(oldFile);
                                }
                            }

                            insertedFileID = await _iFileKiemKeAppService.CreateFile(input.File, null, input.Year, "");
                        }

                        if (insertedFileID != 0)
                        {
                            data.FileId = insertedFileID;
                        }

                        await _yKienRepos.UpdateAsync(data);
                    }
                }
                else
                {
                    var objdata = input.MapTo<YKien>();

                    objdata.FileId = await _iFileKiemKeAppService.CreateFile(input.File, null, input.Year, "");
                    await _yKienRepos.InsertAsync(objdata);
                }
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

        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objdata = await _yKienRepos.FirstOrDefaultAsync(id);

                if (objdata != null)
                {
                    await _yKienRepos.DeleteAsync(objdata);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Ý kiến này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                }
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.Message);
            }
            return commonResponseDto;
        }


        [HttpGet]
        public async Task<IActionResult> DownloadFileById(int fileId)
        {
            var fileEntity = await _fileRepos.FirstOrDefaultAsync(x => x.Id == fileId && x.FileType == FILE_ATTACHMENT);
            if (fileEntity == null)
            {
                return new NotFoundObjectResult(new { Message = "File không tồn tại" });
            }

            var filePath = fileEntity.FilePath;
            if (!System.IO.File.Exists(filePath))
            {
                return new NotFoundObjectResult(new { Message = "File không tồn tại" });
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileStreamResult(memory, Utility.GetContentType(filePath))
            {
                FileDownloadName = fileEntity.FileName
            };
        }
    }
}

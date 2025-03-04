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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace KiemKeDatDai.App.DMBieuMau
{
    /// <summary>
    /// service file kiểm kê
    /// </summary>
    public class FileKiemKeAppService : KiemKeDatDaiAppServiceBase, IFileKiemKeAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<File, long> _fileRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;
        /// <summary>
        /// init function
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="iocResolver"></param>
        /// <param name="fileRepos"></param>
        /// <param name="userRepos"></param>
        /// <param name="objectMapper"></param>
        /// <param name="iUserAppService"></param>
        /// <param name="userRoleRepos"></param>
        /// <param name="httpContextAccessor"></param>
        public FileKiemKeAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<EntitiesDb.File, long> fileRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment
            //ILogAppService iLogAppService
            )
        {
            _fileRepos  = fileRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _webHostEnvironment = webHostEnvironment;
            //_iLogAppService = iLogAppService;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetAll(FileKiemKeInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                _fileRepos.GetAll().WhereIf(!input.Name.IsNullOrEmpty(), x => x.FileName.Contains(input.Name));
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> GetById(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> CreateOrUpdate(FileKiemKeInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                
                    commonResponseDto.Message = "Kỳ thống kê kiểm kê này không tồn tại";
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    return commonResponseDto;
                
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
        }
        [AbpAuthorize]
        public async Task<CommonResponseDto> UploadFile(FileUploadInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (input.File == null || input.File.Length == 0)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "No file uploaded.";
                    return commonResponseDto;
                }

                // Save the file to a directory
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, input.File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.File.CopyToAsync(stream);
                }


                // Save metadata (you can customize this part to save metadata to the database)
                var metadata = JsonConvert.DeserializeObject<Dictionary<string, string>>(input.Metadata);

                // Example: Save file information to the database
                // var fileEntity = new EntitiesDb.File
                // {
                //     FileName = input.File.FileName,
                //     FilePath = filePath,
                //     Metadata = input.Metadata // Assuming you have a Metadata property in your File entity
                // };
                // await _fileRepos.InsertAsync(fileEntity);

                //push message to rabbitmq

                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }
    }
}

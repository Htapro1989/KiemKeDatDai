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

using Microsoft.Extensions.Configuration;

namespace KiemKeDatDai.App.DMBieuMau
{
    /// <summary>
    /// service file kiểm kê
    /// </summary>
    public class FileKiemKeAppService : KiemKeDatDaiAppServiceBase, IFileKiemKeAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<EntitiesDb.File, long> _fileRepos;
        private readonly IRepository<User, long> _userRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IUserAppService _iUserAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserRole, long> _userRoleRepos;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<DonViHanhChinh, long> _donViHanhChinhRepos;
        private readonly RabbitMQService _rabbitMQService;
        private readonly IConfiguration _configuration;
        //private readonly ILogAppService _iLogAppService;

        private readonly ICache mainCache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="iocResolver"></param>
        /// <param name="fileRepos"></param>
        /// <param name="userRepos"></param>
        /// <param name="objectMapper"></param>
        /// <param name="iUserAppService"></param>
        /// <param name="userRoleRepos"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="donViHanhChinhRepos"></param>
        /// <param name="rabbitMQService"></param>
        /// <param name="configuration"></param>
        public FileKiemKeAppService(ICacheManager cacheManager,
            IIocResolver iocResolver,
            IRepository<EntitiesDb.File, long> fileRepos,
            IRepository<User, long> userRepos,
            IObjectMapper objectMapper,
            IUserAppService iUserAppService,
            IRepository<UserRole, long> userRoleRepos,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IRepository<DonViHanhChinh, long> donViHanhChinhRepos,
            RabbitMQService rabbitMQService,
            IConfiguration configuration         //ILogAppService iLogAppService
            )
        {
            _fileRepos = fileRepos;
            _objectMapper = objectMapper;
            _iUserAppService = iUserAppService;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepos = userRoleRepos;
            _webHostEnvironment = webHostEnvironment;
            _donViHanhChinhRepos = donViHanhChinhRepos;
            _rabbitMQService = rabbitMQService;
            _configuration = configuration;

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
        [HttpPost]
        public async Task<CommonResponseDto> UploadFile([FromForm] FileUploadInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objDVHC = _donViHanhChinhRepos.FirstOrDefault(x => x.MaXa == input.MaDVHC & x.Year == input.Year);
                if (objDVHC == null)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Đơn vị hành chính không tồn tại";
                    return commonResponseDto;
                }

                if (objDVHC?.TrangThaiDuyet == (int)CommonEnum.TRANG_THAI_DUYET.DA_DUYET)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Đơn vị hành chính đã được duyệt không thể thêm file";
                    return commonResponseDto;
                }

                if (input.File == null || input.File.Length == 0)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không có file nào được upload.";
                    return commonResponseDto;
                }
                // Check if the file is a ZIP file
                var fileExtension = Path.GetExtension(input.File.FileName).ToLowerInvariant();
                if (fileExtension != ".zip")
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Chỉ chấp nhận file ZIP.";
                    return commonResponseDto;
                }
                // Save the file to a directory
                var uploadsFolder = _configuration["FileUpload:FilePath"];
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(input.File.FileName)}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.File.CopyToAsync(stream);
                }

                //get dvhcid
                var pathDeleted = "";
                var currentFile = _fileRepos.FirstOrDefault(x => x.MaDVHC == input.MaDVHC && x.Year == input.Year && !x.IsDeleted);
                if (currentFile != null)
                {
                    //delete file
                    pathDeleted = currentFile.FilePath;
                    await _fileRepos.DeleteAsync(currentFile);
                }

                var fileEntity = new EntitiesDb.File
                {
                    FileName = input.File.FileName,
                    FilePath = filePath,
                    MaDVHC = input.MaDVHC,
                    Year = input.Year,
                    FileType = CommonEnum.FILE_KYTHONGKE,
                    DVHCId = objDVHC?.Id
                };

                var insertedFileID = await _fileRepos.InsertAndGetIdAsync(fileEntity);

                fileEntity.Id = insertedFileID;
                var fileOutput = _objectMapper.Map<FileKiemKeOuputDto>(fileEntity);
                fileOutput.DeletedFilePath = pathDeleted;
                //push message to rabbitmq
                await _rabbitMQService.SendMessage<FileKiemKeOuputDto>(fileOutput);
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "File upload thành công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.ToString();
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(long year, string maDVHC)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            var fileEntity = await _fileRepos.FirstOrDefaultAsync(x => x.Year == year && x.MaDVHC == maDVHC && x.FileType == CommonEnum.FILE_KYTHONGKE && !x.IsDeleted);
            if (fileEntity == null)
            {
                return new NotFoundObjectResult(new { Message = "File not found on server" });
            }

            var filePath = fileEntity.FilePath;
            if (!System.IO.File.Exists(filePath))
            {
                return new NotFoundObjectResult(new { Message = "File not found on server" });
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileStreamResult(memory, GetContentType(filePath))
            {
                FileDownloadName = fileEntity.FileName
            };
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.ms-word" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" }
            };
        }
    }
}

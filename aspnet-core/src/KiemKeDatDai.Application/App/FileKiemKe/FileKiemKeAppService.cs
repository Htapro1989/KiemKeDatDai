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
using KiemKeDatDai.AppCore.Utility;
using KiemKeDatDai.Authorization;

namespace KiemKeDatDai.RisApplication
{
    /// <summary>
    /// service file kiểm kê
    /// </summary>
    [AbpAuthorize]
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
        private readonly IRepository<ConfigSystem, long> _configSystemRepos;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

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
            IConfiguration configuration,
            IRepository<ConfigSystem, long> configSystemRepos,
            IUnitOfWorkManager unitOfWorkManager     //ILogAppService iLogAppService
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
            _userRepos = userRepos;
            _configSystemRepos = configSystemRepos;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<CommonResponseDto> GetFileKyThongKeByDVHC(FileKiemKeFilterDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objDVHC = await _donViHanhChinhRepos.FirstOrDefaultAsync(input.id) ?? new DonViHanhChinh();
                var lstAllDVHC = await _donViHanhChinhRepos.GetAllListAsync();

                var lstMaXa = new List<string>();
                switch (objDVHC.CapDVHCId)
                {
                    case 0:
                        lstMaXa = lstAllDVHC.Where(x => !string.IsNullOrEmpty(x.MaXa))
                        .Select(x => x.MaXa)
                        .ToList();
                        break;
                    case long v when v > 0 && v < 3:
                        lstMaXa = lstAllDVHC.Join(lstAllDVHC, c => c.MaTinh, p => p.MaTinh,
                        (c, p) => new DonViHanhChinhXaDto
                        {
                            Id = c.Id,
                            MaXa = c.MaXa,
                            Ten = c.Name,
                            Parent_id = p.Parent_id ?? 0
                        })
                        .Where(x => x.Parent_id == input.id && !string.IsNullOrEmpty(x.MaXa))
                        .Select(x => x.MaXa)
                        .ToList()
                        ;

                        break;
                    case 3:
                        lstMaXa = lstAllDVHC
                                .Where(x => x.Parent_id == input.id && !string.IsNullOrEmpty(x.MaXa))
                                .Select(x => x.MaXa)
                                .ToList()
                                ;
                        break;
                    case 4:
                        lstMaXa.Add(objDVHC.MaXa);
                        break;
                }

                var results = await _fileRepos.GetAll()
                .Where(x => lstMaXa.Contains(x.MaDVHC) && x.FileType == CommonEnum.FILE_KYTHONGKE)
                .WhereIf(!string.IsNullOrEmpty(input.Filter), x => x.FileName.Contains(input.Filter.Trim()))
                .Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x => x.CreationTime)
                .ToListAsync();
                var fileDto = _objectMapper.Map<List<FileKiemKeOuputDto>>(results);

                fileDto.ForEach(x =>
                {
                    //Console.WriteLine(x.CreatorUserId);
                    var userNames = _userRepos.FirstOrDefault(x.CreatorUserId).Name;
                    //Console.WriteLine(userNames.Count);
                    x.createName = userNames;
                });
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
                commonResponseDto.ReturnValue = fileDto;
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Console.WriteLine(ex.ToString());
                throw;
            }
            return commonResponseDto;
        }

        public async Task<CommonResponseDto> GetFileAttachByDVHC(FileKiemKeFilterDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objDVHC = await _donViHanhChinhRepos.FirstOrDefaultAsync(input.id) ?? new DonViHanhChinh();

                var results = await _fileRepos.GetAll()
                .Where(x => x.MaDVHC.Equals(objDVHC.Ma) && x.FileType == CommonEnum.FILE_ATTACHMENT)
                .WhereIf(!string.IsNullOrEmpty(input.Filter), x => x.FileName.Contains(input.Filter.Trim()))
                .Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(x => x.CreationTime)
                .ToListAsync();
                var fileDto = _objectMapper.Map<List<FileKiemKeOuputDto>>(results);
                fileDto.ForEach(x =>
                {
                    var userNames = _userRepos.FirstOrDefault(x.CreatorUserId).Name;
                    x.createName = userNames;
                });
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
                commonResponseDto.ReturnValue = fileDto;
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Console.WriteLine(ex.ToString());
                throw;
            }
            return commonResponseDto;
        }

        public async Task<CommonResponseDto> DeleteAttachFile(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var results = await _fileRepos.FirstOrDefaultAsync(x => x.Id == id);
                await _fileRepos.DeleteAsync(results);
                commonResponseDto.Message = "Xóa file thành công.";
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                return commonResponseDto;

            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
        }

        public async Task<CommonResponseDto> CountRequestByCommune(string MaDVHC, int Year)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    var count = await _fileRepos.CountAsync(x => x.MaDVHC == MaDVHC && x.Year == Year && x.FileType == CommonEnum.FILE_KYTHONGKE);
                    var lastUploaded = _fileRepos.GetAll().Where(x => x.MaDVHC == MaDVHC && x.Year == Year && x.FileType == CommonEnum.FILE_KYTHONGKE).OrderByDescending(x => x.CreationTime).FirstOrDefault();
                    if (lastUploaded != null)
                    {
                        commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                        commonResponseDto.Message = "Thành Công";
                        commonResponseDto.ReturnValue = new FileStatisticalOutputDto
                        {
                            UploadFileCount = (int)count,
                            LastUploaded = lastUploaded.CreationTime
                        };
                        return commonResponseDto;
                    }
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "Chưa có File nào được upload";
                    commonResponseDto.ReturnValue = new FileStatisticalOutputDto
                    {
                        UploadFileCount = (int)count,
                        LastUploaded = null
                    };
                }

            }
            catch (Exception ex)
            {
                commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                throw;
            }
            return commonResponseDto;
        }

        [AbpAuthorize(PermissionNames.Pages_Report_UploadAPI)]
        [HttpPost]
        public async Task<CommonResponseDto> UploadFile([FromForm] FileUploadInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();

            // mở transaction
            using (var uow = _unitOfWorkManager.Begin())
            {
                try
                {
                    var objDVHC = _donViHanhChinhRepos.FirstOrDefault(x => x.MaXa == input.MaDVHC & x.Year == input.Year);
                    if (objDVHC == null)
                    {
                        return Fail("Đơn vị hành chính không tồn tại", "DONVIHANHCHINHKHONGTONTAI");
                    }

                    if (objDVHC?.TrangThaiDuyet == (int)CommonEnum.TRANG_THAI_DUYET.DA_DUYET)
                    {
                        return Fail("Đơn vị hành chính đã được duyệt không thể thêm file", "DONVIHANHCHINHDADUYET");
                    }

                    if (input.File == null || input.File.Length == 0)
                    {
                        return Fail("Không có file nào được upload.", "FILEKHONGTONTAI");
                    }

                    var fileExtension = Path.GetExtension(input.File.FileName).ToLowerInvariant();
                    if (fileExtension != ".zip")
                    {
                        return Fail("Chỉ chấp nhận file ZIP.", "SAIDINHDANGFILE");
                    }

                    int _intMaxAllowedSize = objDVHC.MaxFileUpload ?? 0;
                    using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                    {
                        if (_intMaxAllowedSize == 0)
                        {
                            return Warning("Bạn đã hết lượt upload file cho đơn vị hành chính này.", "VUOTQUASOLUONGFILE");
                        }
                    }

                    var lastUploaded = _fileRepos.GetAll()
                        .Where(x => x.MaDVHC == input.MaDVHC && x.Year == input.Year && x.FileType == CommonEnum.FILE_KYTHONGKE)
                        .OrderByDescending(x => x.CreationTime).FirstOrDefault();

                    var currentConfigSystem = await _configSystemRepos.FirstOrDefaultAsync(x => x.Active == true);
                    if (currentConfigSystem != null)
                    {
                        var jsonConfigSystem = JsonConvert.DeserializeObject<JsonConfigSytem>(currentConfigSystem.JsonConfigSystem);
                        if (lastUploaded != null && lastUploaded.CreationTime.AddMinutes(jsonConfigSystem.TimeUpload ?? 0) > DateTime.Now)
                        {
                            return Fail($"Bạn chỉ được phép tải lên một tệp mỗi {jsonConfigSystem.TimeUpload} phút. Vui lòng đợi trước khi tiếp tục.", "FILEDAUPLOAIDUOCUPLOADTRUOCDO");
                        }
                    }

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

                    var pathDeleted = "";
                    var currentFile = _fileRepos.FirstOrDefault(x => x.MaDVHC == input.MaDVHC && x.Year == input.Year && !x.IsDeleted);
                    if (currentFile != null)
                    {
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

                    if (objDVHC != null)
                    {
                        objDVHC.MaxFileUpload -= 1;
                        await _donViHanhChinhRepos.UpdateAsync(objDVHC);
                    }

                    fileEntity.Id = insertedFileID;
                    var fileOutput = _objectMapper.Map<FileKiemKeOuputDto>(fileEntity);
                    fileOutput.DeletedFilePath = pathDeleted;

                    await _rabbitMQService.SendMessage<FileKiemKeOuputDto>(fileOutput);

                    await uow.CompleteAsync(); //  commit nếu không lỗi

                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThanhCong;
                    commonResponseDto.Message = "File upload thành công";
                }
                catch (Exception ex)
                {
                    // rollback tự động nếu không gọi CompleteAsync
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = ex.Message;
                }
            }

            return commonResponseDto;

            // Helper methods
            CommonResponseDto Fail(string message, string code) => new()
            {
                Code = CommonEnum.ResponseCodeStatus.ThatBai,
                Message = message,
                ErrorCode = code
            };

            CommonResponseDto Warning(string message, string code) => new()
            {
                Code = CommonEnum.ResponseCodeStatus.CanhBao,
                Message = message,
                ErrorCode = code
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Report_Upload)]
        [HttpPost]
        public async Task<CommonResponseDto> UploadAttachFile([FromForm] FileAttachUploadInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objDVHC = _donViHanhChinhRepos.FirstOrDefault(x => x.Id == input.DVHCId & x.Year == input.Year);
                if (objDVHC == null)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Đơn vị hành chính không tồn tại";
                    commonResponseDto.ErrorCode = "DONVIHANHCHINHKHONGTONTAI";
                    return commonResponseDto;
                }

                if (input.File == null || input.File.Length == 0)
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Không có file nào được upload.";
                    commonResponseDto.ErrorCode = "FILEKHONGTONTAI";

                    return commonResponseDto;
                }
                // Check if the file is a ZIP file
                var fileExtension = Path.GetExtension(input.File.FileName).ToLowerInvariant();
                string[] fileExtensions = { ".zip", ".zar", ".pdf", ".docx", ".doc", ".xls", ".xlsx", ".dgn" };

                if (!fileExtensions.Contains(fileExtension))
                {
                    commonResponseDto.Code = CommonEnum.ResponseCodeStatus.ThatBai;
                    commonResponseDto.Message = "Chỉ chấp nhận những định dạng sau: Word, Excel, zip, rar, PDF, DGN.";
                    commonResponseDto.ErrorCode = "SAIDINHDANGFILE";

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
                var fileEntity = new EntitiesDb.File
                {
                    FileName = input.File.FileName,
                    FilePath = filePath,
                    MaDVHC = objDVHC.Ma,
                    Year = input.Year,
                    FileType = CommonEnum.FILE_ATTACHMENT,
                    DVHCId = objDVHC?.Id
                };

                var insertedFileID = await _fileRepos.InsertAndGetIdAsync(fileEntity);

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

        [AbpAuthorize(PermissionNames.Pages_Report_DownloadFile)]
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

            return new FileStreamResult(memory, Utility.GetContentType(filePath))
            {
                FileDownloadName = fileEntity.FileName
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Report_DownloadFile)]
        [HttpGet]
        public async Task<IActionResult> DownloadFileByID(int FileId)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            var fileEntity = await _fileRepos.FirstOrDefaultAsync(x => x.Id == FileId);
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

            return new FileStreamResult(memory, Utility.GetContentType(filePath))
            {
                FileDownloadName = fileEntity.FileName
            };
        }

        [AbpAllowAnonymous]
        public async Task<long> CreateFile(IFormFile file, long? dvhcId, long year, string maDvhc = "")
        {

            var uploadsFolder = _configuration["FileUpload:FilePath"];
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Utility.UniqueFileName(file.FileName);

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var fileEntity = new EntitiesDb.File
            {
                FileName = file.FileName,
                FilePath = filePath,
                MaDVHC = maDvhc,
                Year = year,
                FileType = CommonEnum.FILE_Y_KIEN,
                DVHCId = dvhcId != null ? dvhcId : null
            };
            return await _fileRepos.InsertAndGetIdAsync(fileEntity);
        }

    }
}

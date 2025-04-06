﻿using Abp.Application.Services.Dto;
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
using Microsoft.Extensions.Configuration;
using KiemKeDatDai.AppCore.Dto;
namespace KiemKeDatDai.App.DMBieuMau
{
    public class NewsAppService : KiemKeDatDaiAppServiceBase, INewsAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<News, long> _newsRepos;
        private readonly IObjectMapper _objectMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IRepository<EntitiesDb.File, long> _fileRepos;

        private readonly ICache mainCache;

        public NewsAppService(
            IRepository<News, long> newsRepos,
            IObjectMapper objectMapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IRepository<EntitiesDb.File, long> fileRepos
            )
        {
            _objectMapper = objectMapper;
            _httpContextAccessor = httpContextAccessor;
            _newsRepos = newsRepos;
            _configuration = configuration;
            _fileRepos = fileRepos;
        }

        public async Task<CommonResponseDto> GetAll(int type)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var lstBM = new List<NewsDto>();
                var query = await _newsRepos.GetAll().Where(x => x.Type == type).OrderBy(x => x.OrderLabel).ToListAsync();
                var lstNewsDto = _objectMapper.Map<List<NewsDto>>(query);
                commonResponseDto.ReturnValue = lstNewsDto;
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
                var obj = await _newsRepos.FirstOrDefaultAsync(id);
                var objDto = _objectMapper.Map<NewsDto>(obj);
                commonResponseDto.ReturnValue = objDto;
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
        public async Task<CommonResponseDto> GetAllPaging(PagedAndFilteredInputDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var query = _newsRepos.GetAll().WhereIf(!input.Filter.IsNullOrEmpty(), x => x.Title.Contains(input.Filter) || x.Content.Contains(input.Filter) || x.Summary.Contains(input.Filter));
                var totalCount = await query.CountAsync();
                var lstBM = new List<NewsDto>();
                var queryResult = await query.OrderBy(x => x.OrderLabel).Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
                var lstNewsDto = _objectMapper.Map<List<NewsDto>>(queryResult);
                commonResponseDto.ReturnValue = new PagedResultDto<NewsDto>(totalCount, lstNewsDto);
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
        [AbpAuthorize]
        public async Task<CommonResponseDto> CreateOrUpdate([FromForm] NewsUploadDto input)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                if (input.Id != 0)
                {
                    var data = await _newsRepos.FirstOrDefaultAsync(input.Id);
                    if (data != null)
                    {

                        long insertedFileID = 0;
                        if (input.File != null && input.File.Length > 0)
                        {
                            //delete old file
                            if (data.File != null)
                            {
                                var oldFile = await _fileRepos.FirstOrDefaultAsync(data.File.Id);
                                if (oldFile != null)
                                {
                                    System.IO.File.Delete(oldFile.FilePath);
                                    await _fileRepos.DeleteAsync(oldFile);
                                }
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
                                MaDVHC = "",
                                Year = input.Year ?? 0,
                                FileType = CommonEnum.FILE_NEWS,

                            };

                            insertedFileID = await _fileRepos.InsertAndGetIdAsync(fileEntity);

                            //input.FileId = insertedFileID;
                        }
                        //data.File = fileEntity;
                        if (insertedFileID != 0)
                        {
                            data.FileId = insertedFileID;
                        }
                        // data.FileId = insertedFileID;
                        // data = _objectMapper.Map<News>(input);
                        data.Type = input.Type;
                        data.OrderLabel = input.OrderLabel;
                        data.Title = input.Title;
                        data.Content = input.Content;
                        data.Summary = input.Summary;
                        data.Status = input.Status;
                        data.Year = input.Year;
                        data.Active = input.Active;
                        await _newsRepos.UpdateAsync(data);
                    }

                }
                else
                {
                    var objdata = _objectMapper.Map<News>(input);


                    long insertedFileID = 0;
                    if (input.File != null && input.File.Length > 0)
                    {

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
                            MaDVHC = "",
                            Year = input.Year ?? 0,
                            FileType = CommonEnum.FILE_NEWS,
                        };

                        insertedFileID = await _fileRepos.InsertAndGetIdAsync(fileEntity);
                        objdata.File = fileEntity;
                        objdata.FileId = insertedFileID;
                    }

                    var insertedNewsId = await _newsRepos.InsertAndGetIdAsync(objdata);
                }
                commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                commonResponseDto.Message = "Thành Công";
            }
            catch (Exception ex)
            {
                commonResponseDto.Code = ResponseCodeStatus.ThatBai;
                commonResponseDto.Message = ex.Message;
                Logger.Error(ex.ToString());
            }
            return commonResponseDto;
        }

        [AbpAuthorize]
        [HttpDelete]
        public async Task<CommonResponseDto> Delete(long id)
        {
            CommonResponseDto commonResponseDto = new CommonResponseDto();
            try
            {
                var objdata = await _newsRepos.FirstOrDefaultAsync(id);
                if (objdata != null)
                {
                    await _newsRepos.DeleteAsync(objdata);
                    commonResponseDto.Code = ResponseCodeStatus.ThanhCong;
                    //xóa file nếu có
                    if (objdata.FileId != null && objdata.FileId != 0)
                    {
                        var oldFile = await _fileRepos.FirstOrDefaultAsync(objdata.FileId ?? 0);
                        if (oldFile != null)
                        {
                            System.IO.File.Delete(oldFile.FilePath);
                            await _fileRepos.DeleteAsync(oldFile);
                        }
                    }
                    commonResponseDto.Message = "Thành Công";
                }
                else
                {
                    commonResponseDto.Message = "Tin tức này không tồn tại";
                    commonResponseDto.Code = ResponseCodeStatus.ThatBai;
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
    }
}

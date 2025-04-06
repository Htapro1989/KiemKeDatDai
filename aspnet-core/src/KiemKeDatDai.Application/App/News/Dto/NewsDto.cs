using Abp.AutoMapper;
using AutoMapper.Configuration.Annotations;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    [AutoMap(typeof(News))]
    public class NewsDto
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string OrderLabel { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public long FileId { get; set; }
        public int? Status { get; set; }
        public long? Year { get; set; }
        public bool? Active { get; set; }
        public string FileName { get; set; } = "";
        public string CreateName { get; set; } = "";
        public long? CreatorUserId { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
    [AutoMap(typeof(News))]
    public class NewsUploadDto
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string OrderLabel { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int? Status { get; set; }
        public long? Year { get; set; }
        public bool? Active { get; set; }
        [Ignore]
        public IFormFile File { get; set; }
    }
    /// <summary>
    /// Filter for News
    /// </summary>
    public class NewsFilterDto : PagedAndFilteredInputDto
    {
    }
}

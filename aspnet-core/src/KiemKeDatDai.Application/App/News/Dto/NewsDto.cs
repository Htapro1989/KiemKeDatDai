using Abp.AutoMapper;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        public string FileIds { get; set; }
        public int? Status { get; set; }
        public long? Year { get; set; }
        public bool? Active { get; set; }
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
        public long? FileId { get; set; }
        public int? Status { get; set; }
        public long? Year { get; set; }
        public bool? Active { get; set; }
        public IFormFile File { get; set; }
    }
}

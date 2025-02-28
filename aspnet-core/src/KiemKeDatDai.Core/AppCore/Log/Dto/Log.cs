using KiemKeDatDai.AppCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using KiemKeDatDai.EntitiesDb;

namespace KiemKeDatDai.AppCore.Log.Dto
{
    [AutoMap(typeof(KKDD_Log))]
    public class LogInputDto : KKDD_Log
    {
        public long? Id { get; set; }
    }

    public class LogDto : PagedAndFilteredInputDto
    {
        public decimal? UserId { get; set; }
    }
    public class LogOutputDto : KKDD_Log
    {
        public string? UserName { get; set; }
    }
}

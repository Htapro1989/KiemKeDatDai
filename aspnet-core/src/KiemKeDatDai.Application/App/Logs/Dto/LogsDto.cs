using Abp.AutoMapper;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    [AutoMap(typeof(Logs))]
    public class LogsInputDto : Logs
    {
    }
    public class LogsDto : PagedAndFilteredInputDto
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
    public class LogsOuputDto : Logs
    {
    }
}

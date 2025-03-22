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
    [AutoMap(typeof(YKien))]
    public class YKienInputDto : YKien
    {
    }
    public class YKienDto : PagedAndFilteredInputDto
    {
        public string Filter { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DonViCongTac { get; set; }
        public string NoiDungYKien { get; set; }
        public string NoiDungTraLoi { get; set; }
        public long Year { get; set; }
    }
    public class YKienOuputDto : YKien
    {
    }
}

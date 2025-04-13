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
    [AutoMap(typeof(ConfigSystem))]
    public class ConfigSytemInputDto : ConfigSystem
    {
        public bool? IsRequiredFileDGN { get; set; }
        public int? TimeUpload { get; set; }
    }
    public class JsonConfigSytem
    {
        public bool? IsRequiredFileDGN { get; set; }
        public int? TimeUpload { get; set; }
    }
    public class ConfigSystemTime
    {
        public ConfigSystemTime()
        {
            ExpiredTimeToken = "30";
        }
        public string ExpiredTimeToken { get; set; }
    }
}

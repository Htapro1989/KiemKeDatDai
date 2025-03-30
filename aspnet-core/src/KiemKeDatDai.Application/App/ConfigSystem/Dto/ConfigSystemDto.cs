using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.Dto
{
    public class ConfigSystemTime
    {
        public ConfigSystemTime()
        {
            ExpiredTimeToken = "30";
        }
        public string ExpiredTimeToken { get; set; }
    }
}

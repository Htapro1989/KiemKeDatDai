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
    [AutoMap(typeof(CapDVHC))]
    public class CapDVHCInputDto : CapDVHC
    {
    }
    public class CapDVHCDto 
    {
        public int? MaCapDVHC { get; set; }
        public string Filter { get; set; }
    }
    public class CapDVHCOuputDto : CapDVHC
    {
    }
}

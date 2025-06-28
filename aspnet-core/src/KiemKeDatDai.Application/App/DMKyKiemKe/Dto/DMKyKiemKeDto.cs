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
    [AutoMap(typeof(KyThongKeKiemKe))]
    public class DMKyKiemKeInputDto
    {
        public long? Id { get; set; }
        public string? Ma { get; set; }
        public string? Name { get; set; }
        public int? LoaiCapDVHC { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
    public class DMKyKiemKeOuputDto : KyThongKeKiemKe
    {
    }
}

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
    [AutoMap(typeof(File))]
    public class FileKiemKeInputDto : KyThongKeKiemKe
    {
    }
    public class FileKiemKeDto : PagedAndFilteredInputDto
    {
        public long Year { get; set; }
    }
    public class FileiemKeOuputDto : KyThongKeKiemKe
    {
    }
}

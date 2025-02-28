using Abp.AutoMapper;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.AppCore.DanhMucDVHC.Dto
{
    [AutoMap(typeof(DonViHanhChinh))]
    public class DVHCInputDto : DonViHanhChinh
    {
    }
    public class DVHCDto : PagedAndFilteredInputDto
    {
        public long? UserId { get; set; }
        public string DVHCName { get; set; }
    }
    public class DVHCOutputDto : DonViHanhChinh
    {
        public int ChildStatus { get; set; }
    }
}

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
    public class DVHCInput
    {
        public long? UserId { get; set; }
        public long? Year { get; set; }
    }
    public class BaoCaoInPutDto
    {
        public long Year { get; set; }
        public string Ma { get; set; }
    }
    public class BaoCaoDonViHanhChinhOutPutDto
    {
        public long Id { get; set; }
        public string Ten { get; set; }
        public string MaDVHC { get; set; }
        public long? ParentId { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? TongNop { get; set; }
        public int? TongDuyet { get; set; }
        public int? Tong { get; set; }
        public int ChildStatus { get; set; }
    }
}

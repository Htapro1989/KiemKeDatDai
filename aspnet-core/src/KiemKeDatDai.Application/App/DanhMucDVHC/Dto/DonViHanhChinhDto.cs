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
        public long? Year { get; set; }
        public string MaVung { get; set; }
        public string MaTinh { get; set; }
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
        public int? TrangThaiDuyet { get; set; }
        public int? TongNop { get; set; }
        public int? TongDuyet { get; set; }
        public int? Tong { get; set; }
        public int ChildStatus { get; set; }
        public bool? Root { get; set; }
        public bool? IsNopBaoCao { get; set; }
    }

    public class DonViHanhChinhXaDto{
        public long Id { get; set; }
        public string Ten { get; set; }
        public string MaXa { get; set; }
        public long Parent_id { get; set; }
    }
}

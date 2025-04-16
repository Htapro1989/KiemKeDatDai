using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("BieuPhuLucIII")]
    public class BieuPhuLucIII : FullAuditedEntity<long>
    {
        public string? STT { get; set; }
        public string? DienTich { get; set; }
        public string? HienTrang_MLD { get; set; }
        public string? KyTruoc_MLD { get; set; }
        public string? MaLoaiDatSDKetHop { get; set; }
        public string? HienTrang_MDT { get; set; }
        public string? KyTruoc_MDT { get; set; }
        public string? MaKhuVucTongHop { get; set; }
        public string? GhiChu { get; set; }
        public string? MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
    }
}

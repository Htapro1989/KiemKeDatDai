using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu05TKKK")]
    public class Bieu05TKKK : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Nam { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal LUA { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal HNK { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CLN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RDD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RPH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RSX { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NTS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CNT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal LMU { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NKH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ONT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ODT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TSC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CQP { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CAN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DVH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DXH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DYT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DGD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DTT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DKH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DMT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DKT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DNG { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DSK { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SKK { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SKN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SCT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TMD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SKC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SKS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DGT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DTL { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DCT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DPC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DDD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DRA { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DNL { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DBV { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DCH { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DKV { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TON { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TIN { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NTD { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal MNC { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SON { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PNK { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CGT { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal BCS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DCS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal NCS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal MCS { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal GiamKhac { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    [Table("Bieu05TKKK_Xa")]
    public class Bieu05TKKK_Xa : FullAuditedEntity<long>
    {
        public string STT { get; set; }
        public string LoaiDat { get; set; }
        public string Ma { get; set; }
        public decimal Nam { get; set; }
        public decimal LUA { get; set; }
        public decimal HNK { get; set; }
        public decimal CLN { get; set; }
        public decimal RDD { get; set; }
        public decimal RPH { get; set; }
        public decimal RSX { get; set; }
        public decimal NTS { get; set; }
        public decimal CNT { get; set; }
        public decimal LMU { get; set; }
        public decimal NKH { get; set; }
        public decimal ONT { get; set; }
        public decimal ODT { get; set; }
        public decimal TSC { get; set; }
        public decimal CQP { get; set; }
        public decimal CAN { get; set; }
        public decimal DVH { get; set; }
        public decimal DXH { get; set; }
        public decimal DYT { get; set; }
        public decimal DGD { get; set; }
        public decimal DTT { get; set; }
        public decimal DKH { get; set; }
        public decimal DMT { get; set; }
        public decimal DKT { get; set; }
        public decimal DNG { get; set; }
        public decimal DSK { get; set; }
        public decimal SKK { get; set; }
        public decimal SKN { get; set; }
        public decimal SCT { get; set; }
        public decimal TMD { get; set; }
        public decimal SKC { get; set; }
        public decimal SKS { get; set; }
        public decimal DGT { get; set; }
        public decimal DTL { get; set; }
        public decimal DCT { get; set; }
        public decimal DPC { get; set; }
        public decimal DDD { get; set; }
        public decimal DRA { get; set; }
        public decimal DNL { get; set; }
        public decimal DBV { get; set; }
        public decimal DCH { get; set; }
        public decimal DKV { get; set; }
        public decimal TON { get; set; }
        public decimal TIN { get; set; }
        public decimal NTD { get; set; }
        public decimal MNC { get; set; }
        public decimal SON { get; set; }
        public decimal PNK { get; set; }
        public decimal CGT { get; set; }
        public decimal BCS { get; set; }
        public decimal DCS { get; set; }
        public decimal NCS { get; set; }
        public decimal MCS { get; set; }
        public decimal GiamKhac { get; set; }
        public string MaXa { get; set; }
        public long? XaId { get; set; }
        public long Year { get; set; }
        public bool? Active { get; set; }
        public long? sequence { get; set; }
    }
}

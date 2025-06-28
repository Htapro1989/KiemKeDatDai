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
    public interface IBieu05TKKK
    {
        string? STT { get; set; }
        string? LoaiDat { get; set; }
        string? Ma { get; set; }
        decimal Nam { get; set; }
        decimal LUA { get; set; }
        decimal HNK { get; set; }
        decimal CLN { get; set; }
        decimal RDD { get; set; }
        decimal RPH { get; set; }
        decimal RSX { get; set; }
        decimal NTS { get; set; }
        decimal CNT { get; set; }
        decimal LMU { get; set; }
        decimal NKH { get; set; }
        decimal ONT { get; set; }
        decimal ODT { get; set; }
        decimal TSC { get; set; }
        decimal CQP { get; set; }
        decimal CAN { get; set; }
        decimal DVH { get; set; }
        decimal DXH { get; set; }
        decimal DYT { get; set; }
        decimal DGD { get; set; }
        decimal DTT { get; set; }
        decimal DKH { get; set; }
        decimal DMT { get; set; }
        decimal DKT { get; set; }
        decimal DNG { get; set; }
        decimal DSK { get; set; }
        decimal SKK { get; set; }
        decimal SKN { get; set; }
        decimal SCT { get; set; }
        decimal TMD { get; set; }
        decimal SKC { get; set; }
        decimal SKS { get; set; }
        decimal DGT { get; set; }
        decimal DTL { get; set; }
        decimal DCT { get; set; }
        decimal DPC { get; set; }
        decimal DDD { get; set; }
        decimal DRA { get; set; }
        decimal DNL { get; set; }
        decimal DBV { get; set; }
        decimal DCH { get; set; }
        decimal DKV { get; set; }
        decimal TON { get; set; }
        decimal TIN { get; set; }
        decimal NTD { get; set; }
        decimal MNC { get; set; }
        decimal SON { get; set; }
        decimal PNK { get; set; }
        decimal CGT { get; set; }
        decimal BCS { get; set; }
        decimal DCS { get; set; }
        decimal NCS { get; set; }
        decimal MCS { get; set; }
        decimal GiamKhac { get; set; }
        long Year { get; set; }
        bool? Active { get; set; }
        long? sequence { get; set; }
    }
}

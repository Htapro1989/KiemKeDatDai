using Abp.Domain.Entities.Auditing;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    #nullable enable
    [Table("News")]
    public class News : FullAuditedEntity<long>
    {
        public int? Type { get; set; }
        public string? OrderLabel { get; set; } ="";
        public string? Title { get; set; }="";
        public string? Content { get; set; }="";
        public string? Summary { get; set; }="";
        public long? FileId { get; set; }
        public virtual File? File { get; set; }
        
        public int? Status { get; set; }
        public long? Year { get; set; }
        public bool? Active { get; set; }
    }
}

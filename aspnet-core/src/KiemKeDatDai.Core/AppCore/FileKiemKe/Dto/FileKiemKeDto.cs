using Abp.AutoMapper;
using KiemKeDatDai.AppCore.Dto;
using KiemKeDatDai.EntitiesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.ApplicationDto
{
    [AutoMap(typeof(File))]
    public class FileKiemKeInputDto : KyThongKeKiemKe
    {
    }
    public class FileKiemKeFilterDto : PagedAndFilteredInputDto
    {
        public long id{get;set;}
        public long Year { get; set; }
    }
    public class FileStatisticalDto 
    {
        public string MaDVHC { get; set; }
        public int Year { get; set; }
    }
    public class FileStatisticalOutputDto
    {
        public int? UploadFileCount { get; set; }
        public DateTime? LastUploaded { get; set; }
    }
    public class FileKiemKeOuputDto
    {
        public string FileName { get; set; } = "";
        public string FileType { get; set; } = "";
        public string FilePath { get; set; } = "";
        public long Url { get; set; }
        public bool Active { get; set; }
        public long Year { get; set; }
        public string MaDVHC { get; set; } = "";
        public long? DVHCId { get; set; }
        public string DeletedFilePath { get; set; } = "";

        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

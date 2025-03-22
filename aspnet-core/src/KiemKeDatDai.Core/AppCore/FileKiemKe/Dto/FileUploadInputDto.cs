using KiemKeDatDai.EntitiesDb;
using Microsoft.AspNetCore.Http;
namespace KiemKeDatDai.ApplicationDto;

public class FileUploadInputDto
{
    public IFormFile File { get; set; }
    public string MaDVHC { get; set; }
    public int Year { get; set; }
}

public class FileAttachUploadInputDto
{
    public IFormFile File { get; set; }
    public int DVHCId{get;set;}
    public int Year { get; set; }
}
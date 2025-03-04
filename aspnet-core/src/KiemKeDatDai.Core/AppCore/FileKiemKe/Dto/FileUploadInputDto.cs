using KiemKeDatDai.EntitiesDb;
using Microsoft.AspNetCore.Http;

public class FileUploadInputDto: KyThongKeKiemKe
{
    public IFormFile File { get; set; }
    public string Metadata { get; set; }
}
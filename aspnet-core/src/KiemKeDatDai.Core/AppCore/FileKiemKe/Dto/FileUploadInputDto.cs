using KiemKeDatDai.EntitiesDb;
using Microsoft.AspNetCore.Http;

public class FileUploadInputDto
{
    public IFormFile File { get; set; }
    public string MaDVHC { get; set; }
    public int Year { get; set; }
}
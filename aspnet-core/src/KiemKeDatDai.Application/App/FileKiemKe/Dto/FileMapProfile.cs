using KiemKeDatDai.Authorization.Users;
using AutoMapper;
using System.IO;
using KiemKeDatDai.ApplicationDto;

namespace KiemKeDatDai;

public class FileMapProfile : Profile
{
    public FileMapProfile()
    {
        CreateMap<FileKiemKeOuputDto, EntitiesDb.File>().ReverseMap();
        
    }
}

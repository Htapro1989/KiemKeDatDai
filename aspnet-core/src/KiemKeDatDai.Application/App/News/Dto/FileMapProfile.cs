using KiemKeDatDai.Authorization.Users;
using AutoMapper;
using System.IO;
using KiemKeDatDai.ApplicationDto;
using KiemKeDatDai.Dto;
using KiemKeDatDai.EntitiesDb;

namespace KiemKeDatDai;

public class NewsMapProfile : Profile
{
    public NewsMapProfile()
    {
        CreateMap<NewsUploadDto, News>().ForMember(x => x.File, opt => opt.Ignore());
        CreateMap<News, NewsDto>().ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => src.LastModificationTime));
    }
}

using AutoMapper;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<Specialization, RequestSpecializationDto>()
                .ReverseMap();
        }
    }
}

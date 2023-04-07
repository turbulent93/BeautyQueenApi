using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ServiceDto, Service>();
            CreateMap<Service, ServiceDto>();
            CreateMap<RequestServiceDto, Service>();
        }
    }
}

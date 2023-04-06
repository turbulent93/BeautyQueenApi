using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Response;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ServiceDto, ServiceDto>();
            CreateMap<Service, ResponseServiceDto>();
        }
    }
}

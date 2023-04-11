using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<RequestEmployeeDto, Employee>()
                .ForMember(dist => dist.Image, opt => opt.MapFrom(e => e.Image.FileName));
            CreateMap<RequestPutEmployeeDto, Employee>()
                .ForMember(dist => dist.Image, opt => opt.MapFrom(e => e.Image.FileName));
            CreateMap<Employee, EmployeeDto>();
        }
    }
}

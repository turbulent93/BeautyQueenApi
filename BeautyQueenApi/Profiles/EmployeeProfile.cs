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
            CreateMap<RequestEmployeeDto, Employee>();
            CreateMap<RequestPutEmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}

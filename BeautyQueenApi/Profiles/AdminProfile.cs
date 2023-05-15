using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<RequestEmployeeDto, User>();
            CreateMap<RegisterDto, User>()
                .ForMember(dist => dist.Password, opt => opt.MapFrom(e => BCrypt.Net.BCrypt.HashPassword(e.Password)));
        }
    }
}

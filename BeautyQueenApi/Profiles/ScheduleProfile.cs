using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<RequestScheduleDto, Schedule>();
        }
    }
}

using AutoMapper;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<RequestAppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}

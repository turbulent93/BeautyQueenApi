using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> Get();
        Task<AppointmentDto> Post(RequestAppointmentDto appointmentDto);
        Task Delete(int id);
        Task<Appointment?> Find(int id);
    }
}

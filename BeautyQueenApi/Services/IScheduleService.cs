using BeautyQueenApi.Dtos;
using BeautyQueenApi.Dtos.Request;
using BeautyQueenApi.Models;

namespace BeautyQueenApi.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> Get();
        Task<ScheduleDto> Post(RequestScheduleDto scheduleDto);
        Task Delete(int id);
        Task<Schedule?> Find(int id);
        bool ServiceExists(int id);
    }
}

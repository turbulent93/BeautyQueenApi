using BeautyQueenApi.Models;

namespace BeautyQueenApi.Dtos.Request
{
    public class RequestAppointmentDto
    {
        public int EmployeeId { get; set; }
        public int ScheduleId { get; set; }
        public TimeOnly StartAt { get; set; }
        public TimeOnly EndAt { get; set; }
    }
}

using BeautyQueenApi.Models;

namespace BeautyQueenApi.Dtos
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int EmployeeId { get; set; }
    }
}

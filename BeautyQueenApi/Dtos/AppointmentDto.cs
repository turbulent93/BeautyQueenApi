namespace BeautyQueenApi.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ScheduleId { get; set; }
        public TimeOnly StartAt { get; set; }
        public TimeOnly EndAt { get; set; }
    }
}

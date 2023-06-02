using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BeautyQueenApi.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        //[JsonIgnore]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //[JsonIgnore]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        //[JsonIgnore]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public TimeOnly StartAt { get; set; }
        public TimeOnly EndAt { get; set; }
        public string Phone { get; set; }
    }
}

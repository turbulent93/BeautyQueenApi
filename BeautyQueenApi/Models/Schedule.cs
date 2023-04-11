using System.ComponentModel.DataAnnotations;

namespace BeautyQueenApi.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

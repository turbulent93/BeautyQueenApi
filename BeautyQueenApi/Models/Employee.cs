using System.ComponentModel.DataAnnotations;

namespace BeautyQueenApi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

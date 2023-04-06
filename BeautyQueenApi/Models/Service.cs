using System.ComponentModel.DataAnnotations;

namespace BeautyQueenApi.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public List<Employee> Employees { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BeautyQueenApi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public string Image { get; set; }
        public List<Service> Services { get; set; }
    }
}

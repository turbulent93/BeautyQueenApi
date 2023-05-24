using BeautyQueenApi.Models;

namespace BeautyQueenApi.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Specialization Specialization { get; set; }
        public List<Service> Services { get; set; }
        public string Image { get; set; }
    }
}

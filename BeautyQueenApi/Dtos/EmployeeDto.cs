using BeautyQueenApi.Models;

namespace BeautyQueenApi.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int SpecializationId { get; set; }
        public string Image { get; set; }
    }
}

namespace BeautyQueenApi.Dtos.Request
{
    public class RequestEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int SpecializationId { get; set; }
        public IFormFile Image { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}

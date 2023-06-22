using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BeautyQueenApi.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        [JsonIgnore]
        public List<Employee> Employees { get; set; }
        [JsonIgnore]
        public List<Promotion> Promotions { get; set; }
    }
}

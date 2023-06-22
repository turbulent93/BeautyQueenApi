using System.ComponentModel.DataAnnotations;

namespace BeautyQueenApi.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Discount { get; set; }
        public List<Service> Services { get; set; }
        public DateOnly? PeriodFrom { get; set; }
        public DateOnly? PeriodTo { get; set; }
    }
}

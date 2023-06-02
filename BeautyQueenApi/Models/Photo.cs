namespace BeautyQueenApi.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string Source { get; set; }
    }
}

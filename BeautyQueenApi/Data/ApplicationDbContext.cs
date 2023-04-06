using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    }
}

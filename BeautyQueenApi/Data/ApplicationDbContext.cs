using BeautyQueenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyQueenApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
    }
}


using MamunTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace MamunTutorial.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Attendance> Attendence { get; set; }

        public DbSet<Teachers> Teachers { get; set; }

        public DbSet<Packages>  Packages { get; set; }

        public DbSet<Billing> Billings { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<User> UsersInfo { get; set; }
    }
}

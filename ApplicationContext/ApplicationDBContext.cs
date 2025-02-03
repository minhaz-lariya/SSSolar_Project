using Microsoft.EntityFrameworkCore;
using SSSolar_Project.Models;

namespace SSSolar_Project.ApplicationContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            
        }

        public DbSet<AdminMaster> AdminMaster { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SpecialPackages> SpecialPackages { get; set; }
		public DbSet<Notification> Notification { get; set; }
		public DbSet<Sliders> Sliders { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}

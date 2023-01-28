using Medicio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medicio.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)   {   }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}

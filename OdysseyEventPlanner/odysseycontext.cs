using Microsoft.EntityFrameworkCore;
using OdysseyEventPlanner.Models;

namespace OdysseyEventPlanner.Data
{
    public class OdysseyContext : DbContext
    {
        public OdysseyContext(DbContextOptions<OdysseyContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Registrations> Registrations { get; set; }
        public DbSet<ContactFormSubmissions> ContactFormSubmissions { get; set; }
    }
}

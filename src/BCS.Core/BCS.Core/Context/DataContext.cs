using BCS.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCS.Core.Context
{
    public class DataContext(DbContextOptions<DataContext> options)
    : IdentityDbContext<AppUser>(options)
    {
        public DbSet<City> Cityes { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
    }
}

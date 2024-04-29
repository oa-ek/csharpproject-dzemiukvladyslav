using BCS.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BCS.Core.Context
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options)
        {
        }
        public DbSet<City> Cityes { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Structure> Structures { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
        public DbSet<ComplaintComments> ComplaintCommentses { get; set; }
        public DbSet<SuggestionComments> SuggestionCommentses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SuggestionComments>()
              .HasOne(sc => sc.Suggestion)
              .WithMany(s => s.SuggestionCommentses)
              .HasForeignKey(sc => sc.SuggestionId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComplaintComments>()
              .HasOne(sc => sc.Complaint)
              .WithMany(s => s.ComplaintCommentses)
              .HasForeignKey(sc => sc.ComplaintId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}

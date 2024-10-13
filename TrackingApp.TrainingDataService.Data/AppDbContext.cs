using Microsoft.EntityFrameworkCore;
using TrackingApp.TrainingDataService.Domain.Entities;

namespace TrackingApp.TrainingDataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Rank> Ranks { get; set; }
        
        public DbSet<TrainingType> TrainingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Trainee -> Country relationship
            modelBuilder.Entity<Trainee>()
                .HasOne(t => t.Country)
                .WithMany(c => c.Trainees)
                .HasForeignKey(t => t.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Trainee -> Rank relationship
            modelBuilder.Entity<Trainee>()
                .HasOne(t => t.Rank)
                .WithMany(r => r.Trainees)
                .HasForeignKey(t => t.RankId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

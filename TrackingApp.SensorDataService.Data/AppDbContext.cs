using Microsoft.EntityFrameworkCore;
using TrackingApp.SensorDataService.Domain.Entities;

namespace TrackingApp.SensorDataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingData> TrainingDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Training>()
                .HasMany(t => t.TrainingData)
                .WithOne(td => td.Training)
                .HasForeignKey(td => td.TrainingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

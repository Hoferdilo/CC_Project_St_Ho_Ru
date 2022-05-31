using CloudComputingProject.Model;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingProject.Infrastructure
{
    public class TrainDbContext : DbContext
    {
        public DbSet<Station?> Station { get; set; }

        public TrainDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Station>()
                .HasNoDiscriminator()
                .ToContainer(nameof(Station))
                .HasPartitionKey(entity => entity.AdministrativeArea)
                .HasKey(entity => entity.Id);
        }
    }
}

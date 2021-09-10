using _2._NTBrokersDataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace _2._NTBrokersDataBase.Data
{
    public class RealEstateEfCoreContext : DbContext
    {
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<CompanyBroker> CompanyBrokers { get; set; }

        public RealEstateEfCoreContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyBroker>()
                .HasKey(cb => new { cb.CompanyId, cb.BrokerId });
            modelBuilder.Entity<CompanyBroker>()
                .HasOne(cb => cb.Company)
                .WithMany(b => b.CompanyBrokers)
                .HasForeignKey(cb => cb.CompanyId);
            modelBuilder.Entity<CompanyBroker>()
                .HasOne(cb => cb.Broker)
                .WithMany(c => c.CompanyBrokers)
                .HasForeignKey(cb => cb.BrokerId);
        }
    }
}

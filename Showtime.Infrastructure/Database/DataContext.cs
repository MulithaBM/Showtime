using Microsoft.EntityFrameworkCore;
using Showtime.Core.Entities;
using Showtime.Core.ValueObjects;
using Showtime.Infrastructure.ValueConversions;

namespace Showtime.Infrastructure.Database
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<Email>()
                .HaveConversion<EmailConverter>();
            configurationBuilder
                .Properties<Phone>()
                .HaveConversion<PhoneConverter>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}

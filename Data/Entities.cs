using Flyline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Flyline.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();


        public Entities(DbContextOptions<Entities> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

            // add a concurrency token to prevent race condition
            modelBuilder.Entity<Flight>().Property(p => p.RemainingNumberOfSeats)
                .IsConcurrencyToken();

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
        }

    }
}

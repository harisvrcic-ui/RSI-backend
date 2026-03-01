using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.BaseClasses;
using eParking.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace eParking.Data;

public partial class ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : DbContext(options)
{
  
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<MyAppUser> MyAppUsers { get; set; }
    public DbSet<MyAuthenticationToken> MyAuthenticationTokens { get; set; }
    public DbSet<ParkingSpotType> ParkingSpotTypes { get; set; }
    public DbSet<ParkingSpots> ParkingSpots { get; set; }
    public DbSet<ParkingZones> ParkingZones { get; set; }
    public DbSet<Reservations> Reservations { get; set; }
    public DbSet<ReservationType> ReservationTypes { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<Cars> Cars { get; set; }
    public DbSet<Colors> Colors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<MyAppUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<MyAppUser>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Configure Country entity
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name)
            .IsUnique();

        // Configure City entity
        modelBuilder.Entity<City>()
            .HasIndex(c => new { c.Name, c.CountryId })
            .IsUnique();

        modelBuilder.Entity<City>()
            .HasOne(c => c.Country)
            .WithMany()
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParkingSpotType>()
            .Property(p => p.PriceMultiplier)
            .HasPrecision(18, 2);

        // Seed initial data
        modelBuilder.SeedData();
    }




}


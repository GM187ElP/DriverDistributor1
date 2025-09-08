using DriverDistributor.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml;
using Route = DriverDistributor.Entities.Route;

namespace DriverDistributor.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
{
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Distributor> Distributors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<ShipmentNumber> ShipmentNumbers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Personnel>()
            .HasIndex(x => x.Name)
            .IsUnique();

        builder.Entity<Personnel>()
            .HasMany(x => x.Shipments)
            .WithOne(x => x.Personnel)
            .HasForeignKey(x => x.UserName)
            .HasPrincipalKey(x => x.Name)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Driver>()
           .Property(x => x.Name)
            .HasColumnType("nvarchar(100)");

        builder.Entity<Driver>()
          .HasKey(x => x.Name);

        builder.Entity<Distributor>()
          .HasKey(x => x.Name);

        builder.Entity<Distributor>()
            .Property(x => x.Name)
            .HasColumnType("nvarchar(100)");

        base.OnModelCreating(builder);
    }
}

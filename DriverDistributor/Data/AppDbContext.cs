using DriverDistributor.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route = DriverDistributor.Entities.Route;

namespace DriverDistributor.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Distributor> Distributors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<ShipmentNumber> ShipmentNumbers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<SellerRoute> SellerRoutes { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Personnel>()
            .HasIndex(x=>x.Name)
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

        builder.Entity<Customer>()
            .HasMany(x => x.Invoices)
            .WithOne(x => x.Customer)
            .HasForeignKey(x=>x.CustomerCode);

        builder.Entity<Producer>()
            .HasMany(x => x.Invoices)
            .WithOne(x => x.Producer)
            .HasForeignKey(x => x.ProducerCode);

        base.OnModelCreating(builder);
    }
}

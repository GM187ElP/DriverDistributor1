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

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

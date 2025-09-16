using Microsoft.AspNetCore.Identity;

namespace DriverDistributor.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<Shipment> Shipments { get; set; } = [];
}

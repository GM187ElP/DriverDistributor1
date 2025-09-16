using Microsoft.AspNetCore.Identity;

namespace DriverDistributor.Entities
{
    public class ApplicationRole:IdentityRole
    {
        public string? PersianName { get; set; }
    }
}

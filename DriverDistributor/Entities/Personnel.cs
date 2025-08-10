using System.ComponentModel.DataAnnotations;

namespace DriverDistributor.Entities;

public class Personnel
{
    [Key]
    public string PersonnelCode { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Shipment> Shipments { get; set; }
}

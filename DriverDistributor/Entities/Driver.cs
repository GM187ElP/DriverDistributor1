using System.ComponentModel.DataAnnotations;

namespace DriverDistributor.Entities;

public class Driver
{
    [Key]
    public string Name { get; set; } 
    public string? PersonnelCode { get; set; }
    public ICollection<Shipment> Shipments { get; set; } = [];
}

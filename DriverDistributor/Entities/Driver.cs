using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace DriverDistributor.Entities;

public class Driver
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public int? PersonnelCode { get; set; }
    public ICollection<Shipment> Shipments { get; set; } = [];
}

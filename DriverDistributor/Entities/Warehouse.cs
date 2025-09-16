using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace DriverDistributor.Entities;

public class Warehouse
{
    [Key]
    [Label("انبار")]
    public string Name { get; set; } = null!;
    public ICollection<Shipment> Shipments { get; set; } = [];
}

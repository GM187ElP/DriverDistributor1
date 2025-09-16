using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace DriverDistributor.Entities;

public class Route
{
    [Key]
    [Label("آیدی")]
    public int Id { get; set; }
    
    [Label("مسیر")]
    public string? Name { get; set; }

    [Label("مسیر خاص")]
    public bool IsExt { get; set; } = false;
    public ICollection<Shipment> Shipments { get; set; } = [];
}

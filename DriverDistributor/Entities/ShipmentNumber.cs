namespace DriverDistributor.Entities;

public class ShipmentNumber
{
    public long Id { get; set; }
    public int Number { get; set; } = 0;
    public long ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
}

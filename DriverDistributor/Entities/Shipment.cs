using DriverDistributor.Services;

namespace DriverDistributor.Entities;


public class Shipment
{
    public long Id { get; set; }
    public DateTime? ShipmentDateGregorian { get; set; }
    public string ShipmentDatePersian { get; set; }
    public string Weekday { get; set; } 
    public int? DriverPersonnelCode { get; set; }
    public int? DistributorPersonnelCode { get; set; }
    [ForeignKeyLink(nameof(Driver),"Name")]
    public string? DriverName { get; set; }
    [ForeignKeyLink(nameof(Distributor), "Name")]
    public string? DistributorName { get; set; }
    [ForeignKeyLink(nameof(Route), "Name")]
    public string RouteName { get; set; }
    [ForeignKeyLink(nameof(Warehouse), "Name")]
    public string? WarehouseName { get; set; }
    public int? InvoiceCount { get; set; } 
    public long? InvoiceAmount { get; set; } 
    public int? ReturnInvoiceCount { get; set; } = 0;
    public long? ReturnInvoiceAmount { get; set; } = 0;
    public int? SecondServiceInvoiceCount { get; set; } = 0;
    public int? ThirdServiceInvoiceCount { get; set; } = 0;
    public long? SecondServiceInvoiceAmount { get; set; } = 0;
    public long? ThirdServiceInvoiceAmount { get; set; } = 0;
    public bool HasVip { get; set; } = false;
    public bool IsException { get; set; } = false;

    public int? NetInvoiceCount => InvoiceCount + SecondServiceInvoiceCount + ThirdServiceInvoiceCount - ReturnInvoiceCount;
    public long? NetInvoiceAmount => InvoiceAmount + SecondServiceInvoiceAmount + ThirdServiceInvoiceAmount - ReturnInvoiceAmount;

    public Driver Driver { get; set; }
    public Route Route { get; set; }
    public Warehouse Warehouse { get; set; }
    public Distributor Distributor { get; set; }

    public ICollection<ShipmentNumber> ShipmentNumbers { get; set; } = [];
}

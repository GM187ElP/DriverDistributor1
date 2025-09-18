using DriverDistributor.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverDistributor.Entities;


public class Shipment : ISoftDelete
{
    public long Id { get; set; }
    public DateOnly? ShipmentDateGregorian { get; set; }
    public string ShipmentDatePersian { get; set; }
    public string Weekday { get; set; }
    public string MonthName { get; set; }
    public string? DriverPersonnelCode { get; set; }
    public string? DistributorPersonnelCode { get; set; }

    [ForeignKeyLink(nameof(Driver), "Name")]
    public string? DriverName { get; set; }
    public string? DriverDuty { get; set; } = null;

    [ForeignKeyLink(nameof(Distributor), "Name")]
    public string? DistributorName { get; set; }

    [ForeignKeyLink(nameof(Route), "Name")]
    public string? DistributorDuty { get; set; } = null;
    public string RouteName { get; set; }

    [ForeignKeyLink(nameof(Warehouse), "Name")]
    public string? WarehouseName { get; set; }

    public int? InvoiceCount { get; set; }
    public long? InvoiceAmount { get; set; }
    public int? ReturnInvoiceCount { get; set; }
    public long? ReturnInvoiceAmount { get; set; }
    public int? SecondServiceInvoiceCount { get; set; }
    public int? ThirdServiceInvoiceCount { get; set; }
    public long? SecondServiceInvoiceAmount { get; set; }
    public long? ThirdServiceInvoiceAmount { get; set; }

    public bool HasVip { get; set; } = false;
    public bool IsException { get; set; } = false;

    [NotMapped]
    public int? NetInvoiceCount => !IsException ? (InvoiceCount ?? 0) + (SecondServiceInvoiceCount ?? 0) + (ThirdServiceInvoiceCount ?? 0) - (ReturnInvoiceCount ?? 0) : null;

    [NotMapped]
    public long? NetInvoiceAmount => !IsException ? (InvoiceAmount ?? 0) + (SecondServiceInvoiceAmount ?? 0) + (ThirdServiceInvoiceAmount ?? 0) - (ReturnInvoiceAmount ?? 0) : null;

    public string? DistributionCenter { get; set; }
    public string UserName { get; set; }

    public Personnel? Personnel { get; set; }
    public Driver? Driver { get; set; }
    public Route? Route { get; set; }
    public Warehouse? Warehouse { get; set; }
    public Distributor? Distributor { get; set; }

    public ICollection<ShipmentNumber> ShipmentNumbers { get; set; } = new List<ShipmentNumber>();
    public bool IsDeleted { get; set; } = false;
}

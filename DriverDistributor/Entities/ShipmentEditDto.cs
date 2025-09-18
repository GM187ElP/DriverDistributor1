namespace DriverDistributor.Entities;

public class ShipmentEditDto
{
    public long Id { get; set; }
    public DateOnly? ShipmentDateGregorian { get; set; }
    public string ShipmentDatePersian { get; set; }
    public string Weekday { get; set; }
    public string MonthName { get; set; }
    public string? DriverPersonnelCode { get; set; }
    public string? DistributionCenter { get; set; }
    public string? DistributorPersonnelCode { get; set; }
    public string DriverName { get; set; }
    public string DistributorName { get; set; }
    public string RouteName { get; set; }
    public string WarehouseName { get; set; }
    public int? InvoiceCount { get; set; }
    public long? InvoiceAmount { get; set; }
    public int? ReturnInvoiceCount { get; set; }
    public long? ReturnInvoiceAmount { get; set; }
    public List<int?> ShipmentNumbers { get; set; } = [];
    public int? SecondServiceInvoiceCount { get; set; }
    public int? ThirdServiceInvoiceCount { get; set; }
    public long? SecondServiceInvoiceAmount { get; set; }
    public long? ThirdServiceInvoiceAmount { get; set; }
    public bool HasVip { get; set; } = false;
    public bool IsException { get; set; } = false;
    public string? DriverDuty { get; set; } = null;
    public string? DistributorDuty { get; set; } = null;
    public string UserName { get; set; } = string.Empty;
}


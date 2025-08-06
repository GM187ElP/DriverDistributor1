using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DriverDistributor.Entities;

public class ShipmentEditDto
{
    public long Id { get; set; }
    public DateTime? ShipmentDateGregorian { get; set; }
    public string ShipmentDatePersian { get; set; }
    public string Weekday { get; set; }
    public int? DriverPersonnelCode { get; set; }
    public int? DistributorPersonnelCode { get; set; }
    public string DriverName { get; set; }
    public string DistributorName { get; set; }
    public string RouteName { get; set; }
    public string WarehouseName { get; set; }
    public int? InvoiceCount { get; set; }
    public long? InvoiceAmount { get; set; }
    public List<int?> ShipmentNumbers { get; set; } = [];
    public int? ReturnInvoiceCount { get; set; }
    public long? ReturnInvoiceAmount { get; set; }
    public int? SecondServiceInvoiceCount { get; set; }
    public int? ThirdServiceInvoiceCount { get; set; }
    public long? SecondServiceInvoiceAmount { get; set; }
    public long? ThirdServiceInvoiceAmount { get; set; }
    public bool HasVip { get; set; } = false;
    public bool IsException { get; set; } = false;

    public bool Equals(Shipment? other)
    {
        if (other == null)
            return false;

        var properties = typeof(Shipment).GetProperties();

        foreach (var prop in properties)
        {
            var thisValue = prop.GetValue(this);
            var otherValue = prop.GetValue(other);
            if (prop.Name == "ShipmentNumbers")
            {
                var thisNums = (thisValue as string).Split('-') ?? [];
                var otherNums = (otherValue as string).Split('-') ?? [];

                if (!thisNums.All(otherNums.Contains) || !otherNums.All(thisNums.Contains))
                    return false;

                continue;
            }

            if (thisValue == null && otherValue == null)
                continue;

            if (thisValue == null || otherValue == null)
                return false;

            if (!thisValue.Equals(otherValue))
                return false;
        }

        return true;
    }
}

public class ShipmentAddDto
{
    public DateTime? ShipmentDateGregorian { get; set; }
    public string ShipmentDatePersian { get; set; }
    public string Weekday { get; set; }
    public int? DriverPersonnelCode { get; set; }
    public int? DistributorPersonnelCode { get; set; }
    public string DriverName { get; set; }
    public string DistributorName { get; set; }
    public string RouteName { get; set; }
    public string WarehouseName { get; set; }
    public int? InvoiceCount { get; set; }
    public long? InvoiceAmount { get; set; }
    public List<int?> ShipmentNumbers { get; set; } = [];
    public int? ReturnInvoiceCount { get; set; }
    public long? ReturnInvoiceAmount { get; set; }
    public int? SecondServiceInvoiceCount { get; set; }
    public int? ThirdServiceInvoiceCount { get; set; }
    public long? SecondServiceInvoiceAmount { get; set; }
    public long? ThirdServiceInvoiceAmount { get; set; }
    public bool HasVip { get; set; } = false;
    public bool IsException { get; set; } = false;

    public bool Equals(Shipment? other)
    {
        if (other == null)
            return false;

        var properties = typeof(Shipment).GetProperties();

        foreach (var prop in properties)
        {
            var thisValue = prop.GetValue(this);
            var otherValue = prop.GetValue(other);
            if (prop.Name == "ShipmentNumbers")
            {
                var thisNums = (thisValue as string).Split('-') ?? [];
                var otherNums = (otherValue as string).Split('-') ?? [];

                if (!thisNums.All(otherNums.Contains) || !otherNums.All(thisNums.Contains))
                    return false;

                continue;
            }

            if (thisValue == null && otherValue == null)
                continue;

            if (thisValue == null || otherValue == null)
                return false;

            if (!thisValue.Equals(otherValue))
                return false;
        }

        return true;
    }
}
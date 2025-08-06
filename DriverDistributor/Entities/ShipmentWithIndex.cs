namespace DriverDistributor.Entities;

public class ShipmentWithIndex
{
    public int Index { get; set; }
    public Shipment Shipment { get; set; }
    public string ShipmentNumbers => GetShipmentNumbers();


    public string InvoiceCountFormatted => string.Format("{0:N0}", Shipment.InvoiceCount);
    public string InvoiceAmountFormatted => string.Format("{0:N0}", Shipment.InvoiceAmount);
    public string ReturnInvoiceCountFormatted => string.Format("{0:N0}", Shipment.ReturnInvoiceCount);
    public string ReturnInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.ReturnInvoiceAmount);
    public string SecondServiceInvoiceCountFormatted => string.Format("{0:N0}", Shipment.SecondServiceInvoiceCount);
    public string ThirdServiceInvoiceCountFormatted => string.Format("{0:N0}", Shipment.ThirdServiceInvoiceCount);
    public string SecondServiceInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.SecondServiceInvoiceAmount);
    public string ThirdServiceInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.ThirdServiceInvoiceAmount);
    public string NetInvoiceCountFormatted => string.Format("{0:N0}", Shipment.NetInvoiceCount);
    public string NetInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.NetInvoiceAmount);


    string GetShipmentNumbers()
    {
        var list = Shipment?.ShipmentNumbers?
               .Select(x => x.Number) ?? Enumerable.Empty<int>();
        var numbers = string.Join("-", list);
        return numbers;
    }
}

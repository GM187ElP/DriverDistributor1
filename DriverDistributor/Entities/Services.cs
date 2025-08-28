using DriverDistributor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Globalization;

namespace DriverDistributor.Entities;

public class Services
{
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    public Services(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async void SetProperties()
    {
        PersianCalendar persianCalendar = new();
       using var dbContext =await dbContextFactory.CreateDbContextAsync();
        var invoices = dbContext.Invoices;
        foreach (var invoice in invoices)
        {
            var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerCode == invoice.CustomerCode);
            invoice.SellerCode = customer.SellerCode;

            var year = int.Parse(invoice.InvoiceDate.Substring(0, 4));
            var month = int.Parse(invoice.InvoiceDate.Substring(5, 2));
            var day = int.Parse(invoice.InvoiceDate.Substring(8, 2));

            invoice.GregorianInvoiceDate = persianCalendar.ToDateTime(year,month,day,0,0,0,0);       
        }
        await dbContext.SaveChangesAsync();
    }


}

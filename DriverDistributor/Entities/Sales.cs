using System.ComponentModel.DataAnnotations;
using static DriverDistributor.Components.Excel;

namespace DriverDistributor.Entities;

public class Producer
{
    [Key]
    [PropertyInfo(1, "کد تولیدکننده", true, ColumnType.Number)]
    public int ProducerCode { get; set; }

    [PropertyInfo(2, "نام تولیدکننده", true, ColumnType.Text)]
    public string ProducerName { get; set; }

    [PropertyInfo(1000, "-", false, ColumnType.Text)]
    public ICollection<Invoice> Invoices { get; set; }
}

public class Seller
{
    [Key]
    [PropertyInfo(1, "کد فروشنده", true, ColumnType.Number)]
    public int Id { get; set; }

    [PropertyInfo(2, "نام فروشنده", true, ColumnType.Text)]
    public string SellerName { get; set; }

    [PropertyInfo(1000, "-", false, ColumnType.Text)]
    public ICollection<SellerRoute> SellerRoutes { get; set; }
}

public class SellerRoute
{
    [Key]
    [PropertyInfo(1, "کد مسیر", true, ColumnType.Number)]
    public int Id { get; set; }

    [PropertyInfo(2, "نام مسیر", true, ColumnType.Text)]
    public string RouteName { get; set; }

    [PropertyInfo(3, "کد فروشنده", true, ColumnType.Number)]
    public int? SellerId { get; set; }

    [PropertyInfo(1000, "-", false, ColumnType.Text)]
    public Seller? Seller { get; set; }
}

public class Customer
{
    [Key]
    [PropertyInfo(1, "کد مشتري", true, ColumnType.Number)]
    public int CustomerCode { get; set; }

    [PropertyInfo(2, "نام مشتري", true, ColumnType.Text)]
    public string? CustomerName { get; set; }

    [PropertyInfo(3, "شناسه ملی", true, ColumnType.Text)]
    public string? ShenaseMelli { get; set; }

    [PropertyInfo(4, "کد مسیر فروش", true, ColumnType.Number)]
    public int? SellerRouteId { get; set; }

    [PropertyInfo(5, "کد فروشنده", true, ColumnType.Number)]
    public int? SellerCode { get; set; }

    [PropertyInfo(6, "نام فروشنده", true, ColumnType.Text)]
    public string? SellerName { get; set; }

    [PropertyInfo(7, "نشانی", true, ColumnType.Text)]
    public string? Address { get; set; }

    [PropertyInfo(8, "تلفن همراه", true, ColumnType.Text)]
    public string? Mobile { get; set; }

    [PropertyInfo(9, "وضعیت", true, ColumnType.Text)]
    public string? Status { get; set; }

    [PropertyInfo(10, "عنوان", true, ColumnType.Text)]
    public string? Title { get; set; }

    [PropertyInfo(11, "وضعیت خرید", true, ColumnType.Text)]
    public string? PurchaseStatus { get; set; }

    [PropertyInfo(12, "کد مالیاتی", true, ColumnType.Text)]
    public string? TaxingCode { get; set; }

    [PropertyInfo(13, "نام شهرستان", true, ColumnType.Text)]
    public string? CountyName { get; set; }

    [PropertyInfo(14, "کد نقش", true, ColumnType.Number)]
    public long? RoleCode { get; set; }

    [PropertyInfo(15, "مجوز سامانه", true, ColumnType.Text)]
    public string? MojavezSamane { get; set; }

    [PropertyInfo(16, "کد ملی", true, ColumnType.Text)]
    public string? NationalId { get; set; }

    [PropertyInfo(17, "کد پستی", true, ColumnType.Text)]
    public string? PostalCode { get; set; }

    [PropertyInfo(18, "طول جغرافیایی", true, ColumnType.Number)]
    public double? Longitude { get; set; }

    [PropertyInfo(19, "عرض جغرافیایی", true, ColumnType.Number)]
    public double? Latitude { get; set; }

    [PropertyInfo(1000, "-", false, ColumnType.Text)]
    public SellerRoute? SellerRoute { get; set; }

    [PropertyInfo(2000, "-", false, ColumnType.Text)]
    public ICollection<Invoice> Invoices { get; set; }
}

public class Invoice
{
    [PropertyInfo(1, "ردیف", true, ColumnType.Number)]
    public long Id { get; set; }

    [PropertyInfo(2, "شماره فاکتور", true, ColumnType.Number)]
    public int InvoiceNumber { get; set; }

    [PropertyInfo(3, "تاریخ فاکتور", true, ColumnType.Text)]
    public string InvoiceDate { get; set; }

    [PropertyInfo(4, "تاریخ میلادی", true, ColumnType.Text)]
    public DateTime? GregorianInvoiceDate { get; set; }

    [PropertyInfo(5, "کد مشتری", true, ColumnType.Number)]
    public int CustomerCode { get; set; }

    [PropertyInfo(6, "کد تولیدکننده", true, ColumnType.Number)]
    public int ProducerCode { get; set; }

    [PropertyInfo(7, "نوع فاکتور", true, ColumnType.Text)]
    public char InvoiceType { get; set; }

    [PropertyInfo(8, "کد کالا", true, ColumnType.Number)]
    public int ProductCode { get; set; }

    [PropertyInfo(9, "نام کالا", true, ColumnType.Text)]
    public string ProductName { get; set; }

    [PropertyInfo(10, "تعداد کارتن", true, ColumnType.Number)]
    public int CartonQuantity { get; set; }

    [PropertyInfo(11, "تعداد جزء", true, ColumnType.Number)]
    public int UnitQuantity { get; set; }

    [PropertyInfo(12, "تعداد کل", true, ColumnType.Number)]
    public int TotalQuantity { get; set; }

    [PropertyInfo(13, "فی", true, ColumnType.Number)]
    public int UnitPrice { get; set; }

    [PropertyInfo(14, "مبلغ ناخالص", true, ColumnType.Number)]
    public int GrossAmount { get; set; }

    [PropertyInfo(15, "مبلغ تخفیف", true, ColumnType.Number)]
    public int DiscountAmount { get; set; }

    [PropertyInfo(16, "مبلغ پس از تخفیف", true, ColumnType.Number)]
    public int NetAmountAfterDiscount { get; set; }

    [PropertyInfo(17, "مالیات", true, ColumnType.Number)]
    public int Tax { get; set; } = 0;

    [PropertyInfo(18, "مبلغ خالص", true, ColumnType.Number)]
    public int NetAmount { get; set; }

    [PropertyInfo(19, "وزن", true, ColumnType.Number)]
    public double Weight { get; set; }

    [PropertyInfo(20, "کد گروه کالا", true, ColumnType.Number)]
    public int ProductGroupCode { get; set; }

    [PropertyInfo(21, "نام گروه کالا", true, ColumnType.Text)]
    public string ProductGroupName { get; set; }

    [PropertyInfo(22, "کد راننده", true, ColumnType.Number)]
    public int DriverCode { get; set; }

    [PropertyInfo(23, "نام راننده", true, ColumnType.Text)]
    public string DriverName { get; set; }

    [PropertyInfo(24, "کد توزیع‌کننده", true, ColumnType.Number)]
    public int DistributorCode { get; set; }

    [PropertyInfo(25, "نام توزیع‌کننده", true, ColumnType.Text)]
    public string DistributorName { get; set; }

    [PropertyInfo(26, "شماره حواله", true, ColumnType.Number)]
    public int ShipmentNumber { get; set; }

    [PropertyInfo(27, "شماره مرجع برگشتی", true, ColumnType.Number)]
    public int ReturnReferenceNumber { get; set; }

    [PropertyInfo(28, "واحد سنجش", true, ColumnType.Text)]
    public string MeasurementName { get; set; }

    [PropertyInfo(29, "شناسه جامع تجارت", true, ColumnType.Text)]
    public string JameTejaratNumber { get; set; }

    [PropertyInfo(30, "توضیحات", true, ColumnType.Text)]
    public string Description { get; set; }

    [PropertyInfo(1000, "-", false, ColumnType.Text)]
    public Customer? Customer { get; set; }

    [PropertyInfo(2000, "-", false, ColumnType.Text)]
    public Producer? Producer { get; set; }
}

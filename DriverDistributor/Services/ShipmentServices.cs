using System.Globalization;

namespace DriverDistributor.Services;

public class ShipmentServices
{
    public string GregorianToPersian(DateTime myDate, char delimiter = '-')
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(myDate)}{delimiter}{pc.GetMonth(myDate):00}{delimiter}{pc.GetDayOfMonth(myDate):00}";
    }

    public string PersianWeekday(DateTime? date)
    {
        if (date is null)
            return "نا معتبر";
        return date.Value.DayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنجشنبه",
            DayOfWeek.Friday => "جمعه",
            _ => "نا معتبر"
        };
    }

    public static string? GregorianToPersian(DateTime? date)
    {
        if (date is null) return null;
        var pc = new PersianCalendar();
        var d = date.Value;
        return $"{pc.GetYear(d):0000}/{pc.GetMonth(d):00}/{pc.GetDayOfMonth(d):00}";
    }




    //public static bool Equals(Shipment? other)
    //{
    //    if (other == null)
    //        return false;

    //    var properties = typeof(Shipment).GetProperties();

    //    foreach (var prop in properties)
    //    {
    //        var thisValue = prop.GetValue(this);
    //        var otherValue = prop.GetValue(other);
    //        if (prop.Name == "ShipmentNumbers")
    //        {
    //            var thisNums = (thisValue as string).Split(',') ?? [];
    //            var otherNums = (otherValue as string).Split(',') ?? [];

    //            if (!thisNums.All(otherNums.Contains) || !otherNums.All(thisNums.Contains))
    //                return false;

    //            continue;
    //        }

    //        if (thisValue == null && otherValue == null)
    //            continue;



    //        if (thisValue == null || otherValue == null)
    //            return false;

    //        if (!thisValue.Equals(otherValue))
    //            return false;
    //    }

    //    return true;
    //}
}

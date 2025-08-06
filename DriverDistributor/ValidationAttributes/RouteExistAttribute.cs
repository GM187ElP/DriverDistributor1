using DriverDistributor.Services;
using System.ComponentModel.DataAnnotations;

namespace DriverDistributor.ValidationAttributes;

public class RouteExistAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var routeName = value as string;
        if (string.IsNullOrWhiteSpace(routeName))
            return ValidationResult.Success;

        var services = (DataServices)validationContext.GetService(typeof(DataServices));
        List<Route> routes = [];
        routes = services.Load(TableName.Route) as List<Route>;

        // اگر مسیر موجود است خطا بده، در غیر اینصورت معتبر است
        if (routes.Any(x => x.Name == routeName))
        {
            return new ValidationResult("مسیر وارد شده موجود است.", new[] { validationContext.MemberName ?? "RouteName" });
        }

        return ValidationResult.Success;
    }
}


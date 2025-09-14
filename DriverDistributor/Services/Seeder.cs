using DriverDistributor.Components;
using DriverDistributor.Data;
using DriverDistributor.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace DriverDistributor.Services;

public class Seeder
{
    private readonly AppDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly List<Personnel> personnels;

    public Seeder(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
        this.roleManager = roleManager;
        personnels = [];
    }

    public async Task AdminExecuteAsync(string password)
    {
        if (await dbContext.Personnels.AnyAsync(x => x.PersonnelCode == "1"))
            return;

        var personnel = new Personnel
        {
            PersonnelCode = "1",
            Name = "Admin",
            PhoneNumber = ""
        };
        await dbContext.AddAsync(personnel);
        await dbContext.SaveChangesAsync();

        var user = new ApplicationUser
        {
            UserName = "1"
        };
       var result= await userManager.CreateAsync(user, password);

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var role = new ApplicationRole
            {
                Name = "Admin",
                PersianName = "ادمین"
            };
            await roleManager.CreateAsync(role);
        }

        await userManager.AddToRoleAsync(user, "Admin");
    }

    public async Task RoutesExecuteAsync()
    {
        var path = @"E:\C\CS\blazor6\DriverDistributor\DriverDistributor\wwwroot\json\routes.json";
        var json = File.ReadAllText(path);
        var deserializedJson = JsonSerializer.Deserialize<List<RouteJson>>(json);

        var dataList = new List<Entities.Route>();
        foreach (var item in deserializedJson)
        {
            var data = new Entities.Route();
            data.Name = item.Name;
            data.IsExt = item.IsExc;
            dataList.Add(data);
        }

        await dbContext.AddRangeAsync(dataList);
        await dbContext.SaveChangesAsync();
    }

    public async Task PersonnelsExecuteAsync()
    {
        var path = @"E:\C\CS\blazor6\DriverDistributor\DriverDistributor\wwwroot\json\personnels.json";
        var json = File.ReadAllText(path);

        var deserializedJson = JsonSerializer.Deserialize<List<Person>>(json);

        var dataList = new List<Personnel>();

        foreach (var item in deserializedJson)
        {
            var data = new Personnel();
            data.PersonnelCode = item.Id?.ToString(); 
            data.Name = item.Name;
            data.PhoneNumber = "0" + item.Phone;
            dataList.Add(data);
            personnels.Add(data);
        }

        await dbContext.AddRangeAsync(dataList);
        await dbContext.SaveChangesAsync();
    }



    public async Task WarehousesExecuteAsync()
    {
        var path = @"E:\C\CS\blazor6\DriverDistributor\DriverDistributor\wwwroot\json\warehouses.json";
        var json = File.ReadAllText(path);
        var deserializedJson = JsonSerializer.Deserialize<List<string>>(json);

        var dataList = new List<Warehouse>();
        foreach (var item in deserializedJson)
        {
            var data = new Warehouse();
            data.Name = item;
            dataList.Add(data);
        }

        await dbContext.AddRangeAsync(dataList);
        await dbContext.SaveChangesAsync();
    }

    public async Task DDExecuteAsync()
    {
        var path = @"E:\C\CS\blazor6\DriverDistributor\DriverDistributor\wwwroot\json\drivers.json";
        var json = File.ReadAllText(path);
        var deserializedJson = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

        var dataList1 = new List<Driver>();
        var dataList2 = new List<Distributor>();

        foreach (var item in deserializedJson)
        {
            if (item.Key == "drivers")
            {
                foreach (var name in item.Value)
                {
                    var data = new Driver();
                    data.Name = name;

                    var personnel = personnels.FirstOrDefault(x => x.Name == name);
                    if (personnel != null && int.TryParse(personnel.PersonnelCode, out int code))
                        data.PersonnelCode = code;

                    dataList1.Add(data);
                }
            }
            else if (item.Key == "distributors")
            {
                foreach (var name in item.Value)
                {
                    var data = new Distributor();
                    data.Name = name;

                    var personnel = personnels.FirstOrDefault(x => x.Name == name);
                    if (personnel != null && int.TryParse(personnel.PersonnelCode, out int code))
                        data.PersonnelCode = code;

                    dataList2.Add(data);
                }
            }
        }

        await dbContext.AddRangeAsync(dataList1);
        await dbContext.AddRangeAsync(dataList2);
        await dbContext.SaveChangesAsync();
    }

    public class Person
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
    public class RouteJson
    {
        public string Name { get; set; }
        public bool IsExc { get; set; }
    }

}

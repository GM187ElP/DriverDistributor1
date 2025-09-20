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
    private readonly string _root;
    public Seeder(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string root)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
        this.roleManager = roleManager;
        personnels = [];
        _root = root;
    }


    public async Task AdminExecuteAsync(string password)
    {
        var user = new ApplicationUser
        {
            UserName = "1"
        };

        var result = await userManager.CreateAsync(user, password);

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
        var path = Path.Combine(_root, "json", "routes.json");
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

    public async Task PersonnelsExecuteAsync(bool adminExists, bool personnelExist)
    {
        var path = Path.Combine(_root, "json", "personnels.json");
        var json = File.ReadAllText(path);

        var deserializedJson = JsonSerializer.Deserialize<List<Person>>(json);

        var dataList = new List<Personnel>();

        if (!adminExists)
        {
            var personnel = new Personnel
            {
                PersonnelCode = "1",
                Name = "Admin",
                PhoneNumber = ""
            };

            dataList.Add(personnel);
        }

        if (!personnelExist)
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
        var path = Path.Combine(_root, "json", "warehouses.json");

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

    public async Task DriversExecuteAsync()
    {
        var path = Path.Combine(_root, "json", "drivers.json");

        var json = File.ReadAllText(path);
        var deserializedJson = JsonSerializer.Deserialize<List<DD>>(json);

        var dataList1 = new List<Driver>();

        foreach (var item in deserializedJson)
        {

            var data = new Driver();
            data.Name = item.Name;
            data.PersonnelCode = item.PersonnelCode;
            dataList1.Add(data);
        }

        await dbContext.AddRangeAsync(dataList1);
        await dbContext.SaveChangesAsync();
    }

    public async Task DistributorsExecuteAsync()
    {
        var path = Path.Combine(_root, "json", "distributors.json");

        var json = File.ReadAllText(path);
        var deserializedJson = JsonSerializer.Deserialize<List<DD>>(json);

        var dataList1 = new List<Distributor>();

        foreach (var item in deserializedJson)
        {

            var data = new Distributor();
            data.Name = item.Name;
            data.PersonnelCode = item.PersonnelCode;
            dataList1.Add(data);
        }

        await dbContext.AddRangeAsync(dataList1);
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


    public class DD
    {
        public string Name { get; set; }
        public string PersonnelCode { get; set; }
    }

}

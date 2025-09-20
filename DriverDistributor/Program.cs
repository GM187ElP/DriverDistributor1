using MudBlazor.Services;
using DriverDistributor.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DriverDistributor.Data;
using DriverDistributor.Services;
using DriverDistributor.Entities;
using Microsoft.Data.SqlClient;
using MudBlazor;
using System.Globalization;
using Npgsql;
using DocumentFormat.OpenXml.Wordprocessing;

var builder = WebApplication.CreateBuilder(args);
//-------------------------------------------------------------------
builder.Services.AddSingleton<Presence>();

builder.Services.AddSingleton<SecretEncodeDecode>();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddRazorPages(); //  this is required for Identity UI Razor Pages

builder.Services.AddScoped<DataServices>();

var culture = new CultureInfo("fa-IR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
builder.Services.AddTransient<MudLocalizer, CustomeMudLocalizer>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password rules
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    // Username allowed characters
    options.User.AllowedUserNameCharacters = "0123456789";
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });


var secretService = new SecretEncodeDecode(builder.Configuration, builder.Environment);
var dirInfo = Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "Private"));
string path;


string environment;
var localIps = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
string hostIp = "10.11.11.28";

bool isLocal = localIps.Any(ip => ip.ToString() == hostIp);

if (!isLocal)
    environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "production";
else
    environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "production";

var initCatalog = (environment ?? "").ToLower() switch
{
    "development" => "DriverDistributor_Development",
    "staging" => "DriverDistributor_Staging",
    _ => "DriverDistributor"
};

var ssLinuxConnectionString = new SqlConnectionStringBuilder()
{
    DataSource = isLocal ? ".,1433" : $"{hostIp},1433",
    InitialCatalog = initCatalog,
    TrustServerCertificate = true,
    MultipleActiveResultSets = true,
    UserID = "sa",
    Password = "Arsalan.1461",
};

var pgLinuxConnectionString = new NpgsqlConnectionStringBuilder()
{
    Host = "10.11.11.28",
    Port = 5432,
    Database = initCatalog,
    Username = "postgres",
    Password = "Arsalan.1461",
    //SslMode = SslMode.Require
};

var database = "pg"; // ss or pg
var selector = "linux";
var connectionString = string.Empty;
if (builder.Environment.IsDevelopment())
{
    //--------------------------encryption------------------------------------
    //SecretEncodeDecode.EncodeToJson();

    if (selector == "linux")
    {
        connectionString = database == "ss" ? ssLinuxConnectionString.ToString() : pgLinuxConnectionString.ToString();
    }
    else
    {
        if (selector == "local")
        {
            //--------------------------sqlserver------------------------------------------
            connectionString = builder.Configuration["ConnectionStrings:Local"];
        }
        else
        {
            //--------------------------decryption--------------------------------------------
            path = Path.Combine(dirInfo.FullName, "secrets.Remote.Remote.json");   // using remote database for local project
            connectionString = secretService.DecodeToJson(path).ToString();
        }
    }
}
else
{
    if (selector == "linux")
    {
        connectionString = database == "ss" ? ssLinuxConnectionString.ToString() : pgLinuxConnectionString.ToString();
    }
    else
    {
        //--------------------------decryption--------------------------------------------
        path = Path.Combine(dirInfo.FullName, "secrets.Remote.Local.json");    // using in hosted env
        connectionString = secretService.DecodeToJson(path).ToString();
    }
}

if (database == "ss")
    builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));
else
    builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseNpgsql(connectionString));

//-------------------------------------------------------------------

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
//-------------------------------------------------------------------
app.UseAuthentication();
app.UseAuthorization();


//-------------------------------------------------------------------
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
//-------------------------------------------------------------------


app.MapRazorPages(); // this makes /Identity/Account/Login work
app.MapControllers();
app.MapHub<PresenceHub>("/presenceHub");

/*
using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    //await dbContext.Warehouses.ExecuteDeleteAsync();
    //await dbContext.Routes.ExecuteDeleteAsync();
    //await dbContext.Drivers.ExecuteDeleteAsync();
    //await dbContext.Distributors.ExecuteDeleteAsync();
    //await dbContext.Personnels.ExecuteDeleteAsync();

    var root = app.Environment.WebRootPath;
    var seeder = new Seeder(dbContext, userManager, roleManager, root);

    if (!await userManager.Users.AnyAsync(x => x.UserName == "1"))
    {
        string password = "";
        await seeder.AdminExecuteAsync(password);
    }
    var personnelExist = await dbContext.Personnels.CountAsync() >= 94;
    var adminExists = await dbContext.Personnels.AnyAsync(x => x.PersonnelCode == "1");
    if (!dbContext.Warehouses.Any())
        await seeder.WarehousesExecuteAsync();
    if (!dbContext.Routes.Any())
        await seeder.RoutesExecuteAsync();
    await seeder.PersonnelsExecuteAsync(adminExists, personnelExist);
    if (!dbContext.Drivers.Any())
        await seeder.DriversExecuteAsync();
    if (!dbContext.Distributors.Any())
        await seeder.DistributorsExecuteAsync();
}
//-------------------------------------------------------------------
*/

app.Run();


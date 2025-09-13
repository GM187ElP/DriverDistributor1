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

string hostIp = "10.11.11.28";
bool isLocal = System.Net.Dns.GetHostAddresses("localhost")
                 .Any(ip => ip.ToString() == hostIp);

string migrationEnvironment = "Development";
var initCatalog = migrationEnvironment switch 
{ 
    "Development"=> "DriverDistributor_Dev",
    "Staging" => "DriverDistributor_Staging",
    _=> "DriverDistributor"
};

var linuxConnectionString = new SqlConnectionStringBuilder()
{
    DataSource = isLocal ? ".,1433" : $"{hostIp},1433",
    InitialCatalog = initCatalog,
    TrustServerCertificate = true,
    MultipleActiveResultSets = true,
    UserID = "sa",
    Password = "Arsalan.1461",
};


var selector = "linux";
var connectionString = string.Empty;
if (builder.Environment.IsDevelopment())
{
    //--------------------------encryption------------------------------------
    //SecretEncodeDecode.EncodeToJson();

    if (selector == "linux")
    {
        connectionString = linuxConnectionString.ToString();
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
        connectionString = linuxConnectionString.ToString();
    }
    else
    {
        //--------------------------decryption--------------------------------------------
        path = Path.Combine(dirInfo.FullName, "secrets.Remote.Local.json");    // using in hosted env
        connectionString = secretService.DecodeToJson(path).ToString();
    }
}

builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));

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
//-------------------------------------------------------------------

app.Run();


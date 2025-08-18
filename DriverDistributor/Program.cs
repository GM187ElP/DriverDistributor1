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
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//-------------------------------------------------------------------

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<SecretEncodeDecode>();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddRazorPages(); //  this is required for Identity UI Razor Pages

builder.Services.AddScoped<DataServices>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password rules
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    // Username allowed characters
    options.User.AllowedUserNameCharacters = "0123456789";

    // You can also configure other options here
});

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });


var secretService = new SecretEncodeDecode(builder.Configuration, builder.Environment);
var dirInfo = Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "Private"));
string path;

if (builder.Environment.IsDevelopment())
{
    //--------------------------sqlite------------------------------------------
    //var dbPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "App_Data", "1404.db");
    //builder.Services.AddDbContext<AppDbContext>(options =>
    //    options.UseSqlite($"Data Source={dbPath}"));

    //--------------------------sqlserver------------------------------------------
    //var connectionString = builder.Configuration["ConnectionStrings:Local"];
    //builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));

    //--------------------------encryption------------------------------------
    //SecretEncodeDecode.EncodeToJson();

    //--------------------------decryption--------------------------------------------
    path = Path.Combine(dirInfo.FullName, "secrets.Remote.Remote.json");   // using remote database for local project
    var connectionString = secretService.DecodeToJson(path).ToString();
    builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));
}
else
{
    //--------------------------decryption--------------------------------------------
    path = Path.Combine(dirInfo.FullName, "secrets.Remote.Local.json");    // using in hosted env
    var connectionString = secretService.DecodeToJson(path).ToString();
    builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));
}


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
//-------------------------------------------------------------------

app.Run();


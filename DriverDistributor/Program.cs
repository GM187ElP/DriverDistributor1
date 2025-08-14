using MudBlazor.Services;
using DriverDistributor.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DriverDistributor.Data;
using DriverDistributor.Services;
using DriverDistributor.Entities;

var builder = WebApplication.CreateBuilder(args);
//-------------------------------------------------------------------
var env = builder.Environment;
var dbPath = Path.Combine(env.ContentRootPath, "wwwroot", "App_Data", "1404.db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

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

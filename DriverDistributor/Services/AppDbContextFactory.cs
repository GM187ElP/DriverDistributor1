using DriverDistributor.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace DriverDistributor.Services;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var initCatalog = env switch
        {
            "Development" => "DriverDistributor_Dev",
            "Staging" => "DriverDistributor_Staging",
            _ => "DriverDistributor"
        };

        

        string hostIp = "10.11.11.28";
        bool isLocal = System.Net.Dns.GetHostAddresses("localhost")
                         .Any(ip => ip.ToString() == hostIp);

        var connString = new SqlConnectionStringBuilder
        {
            DataSource = isLocal ? ".,1433" : $"{hostIp},1433",
            InitialCatalog = initCatalog,
            UserID = "sa",
            Password = "Arsalan.1461",
            TrustServerCertificate = true,
            MultipleActiveResultSets = true
        }.ToString();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connString)
            .Options;


        return new AppDbContext(options);
    }
}

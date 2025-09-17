using DriverDistributor.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DriverDistributor.Services;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var initCatalog = env switch
        {
            "Development" => "DriverDistributor_Development",
            "Staging" => "DriverDistributor_Staging",
            _ => "DriverDistributor"
        };

        

        string hostIp = "10.11.11.28";
        bool isLocal = System.Net.Dns.GetHostAddresses("localhost")
                         .Any(ip => ip.ToString() == hostIp);

        var db = "pg";
        var connString = new SqlConnectionStringBuilder
        {
            DataSource = isLocal ? ".,1433" : $"{hostIp},1433",
            InitialCatalog = initCatalog,
            UserID = "sa",
            Password = "Arsalan.1461",
            TrustServerCertificate = true,
            MultipleActiveResultSets = true
        };

        var pgLinuxConnectionString = new NpgsqlConnectionStringBuilder()
        {
            Host = isLocal ? "localhost" : $"{hostIp}",
            Port = 5432,
            Database = initCatalog,
            Username = "postgres",
            Password = "Arsalan.1461",
            //SslMode = SslMode.Require
        };

        DbContextOptions<AppDbContext> options;
        if(db=="ss")
         options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connString.ToString())
            .Options;
        else
            options = new DbContextOptionsBuilder<AppDbContext>()
               .UseNpgsql(pgLinuxConnectionString.ToString())
               .Options;

        return new AppDbContext(options);
    }
}

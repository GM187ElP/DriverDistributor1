using DriverDistributor.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DriverDistributor.Services;

public class DataServices
{
    private readonly AppDbContext _dbContext;

    public DataServices(AppDbContext dbContext) => _dbContext = dbContext;

    public  object Load(TableName tableName)
    {
        object data = tableName switch
        {
            TableName.Driver => _dbContext.Drivers.ToList(),
            TableName.Distributor => _dbContext.Distributors.ToList(),
            TableName.Route => _dbContext.Routes.ToList(),
            TableName.Warehouse => _dbContext.Warehouses.ToList(),
            TableName.Shipments => _dbContext.Shipments.ToList(),
        };
        return data;
    }

    public EntityEntry<T> Add<T>(T data) where T : class
    {
        var entry = _dbContext.Set<T>().Add(data);
        _dbContext.SaveChanges();
        return entry;
    }
}

public enum TableName
{
    Driver, Distributor, Route, Warehouse, Shipments
}

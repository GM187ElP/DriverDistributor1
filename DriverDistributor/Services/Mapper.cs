using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace DriverDistributor.Services;

public class Mapper
{
    public static void MapFromDto<TDto, TEntity>(TDto dto, TEntity entity)
    {
        var dtoProps = typeof(TDto).GetProperties();
        var entityProps = typeof(TEntity).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var entityProp = entityProps.FirstOrDefault(p =>
                p.Name == dtoProp.Name &&
                p.PropertyType == dtoProp.PropertyType &&
                p.CanWrite);

            if (entityProp != null)
            {
                var value = dtoProp.GetValue(dto);
                entityProp.SetValue(entity, value);
            }
        }
    }

    public static void MapToDto<TEntity, TDto>(TEntity entity, TDto dto)
    {
        var dtoProps = typeof(TDto).GetProperties();
        var entityProps = typeof(TEntity).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var entityProp = entityProps.FirstOrDefault(p =>
                p.Name == dtoProp.Name &&
                p.PropertyType == dtoProp.PropertyType &&
                dtoProp.CanWrite);

            if (entityProp != null)
            {
                var value = entityProp.GetValue(entity);
                dtoProp.SetValue(dto, value);
            }
        }
    }
    public static async Task MapDtoToEntityWithNavigations<TDto, TEntity>(TDto dto, TEntity entity, DbContext dbContext)
    {
        // 1. Map simple properties synchronously
        MapFromDto(dto, entity);

        // 2. Map navigation properties asynchronously
        await AutoMapNavigationsFromAttributes(entity, dbContext);
    }

    

    private static async Task AutoMapNavigationsFromAttributes(object entity, DbContext dbContext)
    {
        var entityType = entity.GetType();
        var propsWithAttr = entityType
            .GetProperties()
            .Select(p => new
            {
                Property = p,
                Attr = p.GetCustomAttribute<ForeignKeyLinkAttribute>()
            })
            .Where(x => x.Attr != null);

        foreach (var item in propsWithAttr)
        {
            var fkProp = item.Property;
            var navName = item.Attr.NavigationProperty;
            var keyName = item.Attr.NavigationKeyProperty;

            var navProp = entityType.GetProperty(navName);
            if (navProp == null)
                continue;

            var fkValue = fkProp.GetValue(entity);
            if (fkValue == null)
                continue;

            // Get navigation entity type
            var navType = navProp.PropertyType;

            // Find DbSet<T> property on DbContext where T == navType
            var dbSetProp = dbContext.GetType()
                .GetProperties()
                .FirstOrDefault(p =>
                    p.PropertyType.IsGenericType &&
                    p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                    p.PropertyType.GenericTypeArguments[0] == navType);

            if (dbSetProp == null)
                continue;

            var dbSet = dbSetProp.GetValue(dbContext);
            if (dbSet == null)
                continue;

            // Find key property on navigation type
            var keyProp = navType.GetProperty(keyName);
            if (keyProp == null)
                continue;

            // Build expression: x => x.KeyProperty == fkValue
            var parameter = Expression.Parameter(navType, "x");
            var body = Expression.Equal(
                Expression.Property(parameter, keyProp),
                Expression.Constant(fkValue)
            );
            var lambda = Expression.Lambda(body, parameter);

            // Find correct FirstOrDefaultAsync overload
            var methods = typeof(EntityFrameworkQueryableExtensions)
        .GetMethods()
        .Where(m => m.Name == "FirstOrDefaultAsync");

            MethodInfo method = null;

            foreach (var m in methods)
            {
                var methodParams = m.GetParameters();
                if (methodParams.Length == 2)
                {
                    if (methodParams[0].ParameterType.IsGenericType &&
                        methodParams[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>) &&
                        methodParams[1].ParameterType.IsGenericType &&
                        methodParams[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>))
                    {
                        method = m;
                        break;
                    }
                }
                else if (methodParams.Length == 3)
                {
                    if (methodParams[0].ParameterType.IsGenericType &&
                        methodParams[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>) &&
                        methodParams[1].ParameterType.IsGenericType &&
                        methodParams[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>) &&
                        methodParams[2].ParameterType == typeof(CancellationToken))
                    {
                        method = m;
                        break;
                    }
                }
            }

            if (method == null)
                throw new InvalidOperationException("Cannot find suitable FirstOrDefaultAsync method");

            method = method.MakeGenericMethod(navType);

            object[] parameters;

            if (method.GetParameters().Length == 3)
                parameters = new object[] { dbSet, lambda, CancellationToken.None };
            else
                parameters = new object[] { dbSet, lambda };

            var resultTask = (Task)method.Invoke(null, parameters);
            await resultTask.ConfigureAwait(false);

        }
    }
}


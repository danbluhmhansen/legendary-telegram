namespace BlazorApp1.Server.Helpers;

using System.Reflection;

using Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static IQueryable Set(this DbContext dbContext, Type entityType)
    {
        MethodInfo? methodInfo = typeof(DbContext).GetMethod(nameof(DbContext.Set), Array.Empty<Type>());

        if (methodInfo is null)
            throw new DbSetNotFoundException(entityType);

        object? query = methodInfo.MakeGenericMethod(entityType).Invoke(dbContext, null);

        if (query is null)
            throw new DbSetNotFoundException(entityType);

        return (IQueryable)query;
    }
}

public class DbSetNotFoundException : Exception
{
    public DbSetNotFoundException(Type entityType) : base($"DbSet of type {entityType.Name} could not be found.") { }

    public DbSetNotFoundException(Type entityType, Exception? innerException)
        : base($"DbSet of type {entityType.Name} could not be found.", innerException) { }
}

namespace BlazorApp1.Server;

using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using BlazorApp1.Server.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class CustomSaveChangesInterceptor : ISaveChangesInterceptor
{
    public CustomSaveChangesInterceptor(
        DateTimeProvider dateTimeProvider,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.httpContextAccessor = httpContextAccessor;
        this.userManager = userManager;
    }

    private readonly DateTimeProvider dateTimeProvider;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly UserManager<ApplicationUser> userManager;

    public void SaveChangesFailed(DbContextErrorEventData eventData) => throw new NotImplementedException();

    public Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result) => throw new NotImplementedException();

    public ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return result;

        string? userId = null;
        if (this.httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.httpContextAccessor.HttpContext.User);
            userId = user.Id;
        }

        IEnumerable<AuditLog> auditLogs = eventData.Context.ChangeTracker.Entries()
            .Where((EntityEntry entry) => entry.State is EntityState.Deleted
                or EntityState.Modified or EntityState.Added)
            .Select((EntityEntry entry) => new AuditLog
            {
                AuditDate = this.dateTimeProvider.UtcNow,
                AuditType = $"Entity{entry.State}",
                AuditUserId = userId,
                EntityKey = JsonSerializer.Serialize(entry.Properties
                    .Where((PropertyEntry property) => property.Metadata.IsPrimaryKey())
                    .ToDictionary()),
                EntityData = JsonSerializer.Serialize(entry.State switch
                {
                    EntityState.Deleted => entry.Properties
                        .Where((PropertyEntry property) => !property.Metadata.IsPrimaryKey())
                        .ToDictionary(),
                    EntityState.Modified => entry.Properties
                        .Where((PropertyEntry property) => !property.Metadata.IsPrimaryKey()
                            && !Equals(property.OriginalValue, property.CurrentValue))
                        .ToDictionary(),
                    EntityState.Added => entry.Properties
                        .Where((PropertyEntry property) => property.Metadata.IsPrimaryKey())
                        .ToDictionary(),
                    _ => throw new NotSupportedException(),
                }),
            });

        eventData.Context.AddRange(auditLogs);

        return result;
    }
}

internal static class DictionaryExtensions
{
    public static IDictionary<string, object?> ToDictionary(this IEnumerable<PropertyEntry> properties) =>
        properties.ToDictionary(
            (PropertyEntry property) => property.Metadata.Name,
            (PropertyEntry property) => property.OriginalValue);
}

namespace BlazorApp1.Server;

using Audit.Core;
using Audit.EntityFramework;

using Microsoft.EntityFrameworkCore.Diagnostics;

public class CustomAuditSaveChangesInterceptor : SaveChangesInterceptor
{
    public CustomAuditSaveChangesInterceptor(DbContextHelper helper, IAuditDbContext auditContext)
    {
        this.helper = helper;
        this.auditContext = auditContext;
    }

    private readonly DbContextHelper helper;
    private readonly IAuditDbContext auditContext;
    private IAuditScope? auditScope;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        this.auditScope = this.helper.BeginSaveChanges(this.auditContext);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        this.auditScope = await this.helper.BeginSaveChangesAsync(this.auditContext);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        this.helper.EndSaveChanges(this.auditContext, this.auditScope, result);
        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        await this.helper.EndSaveChangesAsync(this.auditContext, this.auditScope, result);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        this.helper.EndSaveChanges(this.auditContext, this.auditScope, 0, eventData.Exception);
        base.SaveChangesFailed(eventData);
    }

    public override async Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        await this.helper.EndSaveChangesAsync(this.auditContext, this.auditScope, 0, eventData.Exception);
        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}

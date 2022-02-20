namespace BlazorApp1.Server.Entities;

public record AuditLog
{
    public string? AuditType { get; set; }
    public DateTime AuditDate { get; set; }

    public string? EntityKey { get; set; }
    public string? EntityData { get; set; }

    public string? AuditUserId { get; set; }
    public virtual ApplicationUser? AuditUser { get; set; }
}

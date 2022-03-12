namespace BlazorApp1.Server;

public class DateTimeProvider
{
    public virtual DateTime Now => DateTime.Now;
    public virtual DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    public virtual DateTime UtcNow => DateTime.UtcNow;
}

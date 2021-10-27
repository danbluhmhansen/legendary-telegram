namespace BlazorApp1.Server.Models;

public abstract class CustomerBase
{
	public int Id { get; set; }
	public string? ApiVersion { get; set; }
}

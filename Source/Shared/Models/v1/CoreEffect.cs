namespace BlazorApp1.Shared.Models.v1;

public record CoreEffect
{
	public Guid Id { get; set; }
	public string? Path { get; set; }
	public string? Rule { get; set; }

	public Guid CharacterId { get; set; }
	public Character? Character { get; set; }
}

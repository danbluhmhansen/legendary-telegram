namespace BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

[Key(nameof(Id))]
public record CoreEffect
{
	public Guid Id { get; init; }
	public string? Path { get; set; }
	public string? Rule { get; set; }

	public Guid CharacterId { get; set; }
	public Character? Character { get; set; }
}

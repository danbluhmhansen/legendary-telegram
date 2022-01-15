namespace BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

[Key(nameof(Id))]
public record Effect
{
	public Guid Id { get; init; }
	public string? Name { get; set; }
	public string? Path { get; set; }
	public string? Rule { get; set; }

	public Guid FeatureId { get; set; }
	public Feature? Feature { get; set; }
}

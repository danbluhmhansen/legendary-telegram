namespace BlazorApp1.Shared.Models.v1;

public record Effect
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? Path { get; set; }
	public string? Rule { get; set; }

	public Guid FeatureId { get; set; }
	public Feature? Feature { get; set; }
}

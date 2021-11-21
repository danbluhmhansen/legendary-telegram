namespace BlazorApp1.Shared.Models.v1;

public record Character
{
	public Guid Id { get; init; }
	public string? Name { get; set; }

	public ICollection<Feature> Features { get; set; } = new List<Feature>();
	public ICollection<CoreEffect> Effects { get; set; } = new List<CoreEffect>();
}

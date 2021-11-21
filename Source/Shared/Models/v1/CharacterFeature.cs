namespace BlazorApp1.Shared.Models.v1;

public record CharacterFeature
{
	public Guid CharacterId { get; set; }
	public Character? Character { get; set; }
	public Guid FeatureId { get; set; }
	public Feature? Feature { get; set; }
}

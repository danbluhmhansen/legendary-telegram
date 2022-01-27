namespace BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

[Key(nameof(CharacterId), nameof(FeatureId))]
public record CharacterFeature
{
	public Guid CharacterId { get; set; }
	public Character? Character { get; set; }
	public Guid FeatureId { get; set; }
	public Feature? Feature { get; set; }
}

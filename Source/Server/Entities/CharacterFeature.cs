
namespace BlazorApp1.Server.Entities;

public record CharacterFeature
{
	public Guid CharacterId { get; init; }
	public virtual Character? Character { get; init; }

	public Guid FeatureId { get; init; }
	public virtual Feature? Feature { get; init; }
}

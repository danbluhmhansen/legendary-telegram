namespace BlazorApp1.Server.Entities;

using System.Text.Json;

public record CoreEffect
{
	public CoreEffect() { }

	public CoreEffect(Guid id, string path, JsonElement rule, Guid characterId, Character character)
	{
		this.Id = id;
		this.Path = path;
		this.Rule = rule;
		this.CharacterId = characterId;
		this.Character = character;
	}

	public Guid Id { get; init; }
	public string? Path { get; set; }
	public JsonElement Rule { get; set; }

	public Guid CharacterId { get; set; }
	public virtual Character? Character { get; set; }
}

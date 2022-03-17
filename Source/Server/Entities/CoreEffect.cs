namespace BlazorApp1.Server.Entities;

using System.Text.Json.Nodes;

public record CoreEffect
{
    public CoreEffect(Guid id, string path, JsonObject rule, Guid characterId, Character character)
    {
        this.Id = id;
        this.Path = path;
        this.Rule = rule;
        this.CharacterId = characterId;
        this.Character = character;
    }

    public CoreEffect() { }

    public Guid Id { get; set; }
    public string? Path { get; set; }
    public JsonObject? Rule { get; set; }

    public Guid CharacterId { get; set; }
    public virtual Character? Character { get; set; }
}

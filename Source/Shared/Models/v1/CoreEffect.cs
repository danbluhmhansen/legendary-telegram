namespace BlazorApp1.Shared.Models.v1;

using System.Text.Json.Nodes;

using Microsoft.OData.Client;

[Key(nameof(Id))]
public record CoreEffect
{
    public Guid Id { get; set; }
    public string? Path { get; set; }
    public JsonObject? Rule { get; set; }

    public Guid CharacterId { get; set; }
    public Character? Character { get; set; }
}

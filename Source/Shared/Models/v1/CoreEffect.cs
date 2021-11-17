namespace BlazorApp1.Shared.Models.v1;

using System.Text.Json;

public record CoreEffect(Guid Id, string Path, JsonElement Rule, Guid CharacterId, Character Character);

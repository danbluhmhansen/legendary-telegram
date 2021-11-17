namespace BlazorApp1.Shared.Models.v1;

using System.Text.Json;

public record Effect(Guid Id, string Name, string Path, JsonElement Rule, Guid FeatureId, Feature Feature);

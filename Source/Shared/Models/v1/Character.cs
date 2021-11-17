namespace BlazorApp1.Shared.Models.v1;

public record Character(Guid Id, string Name, ICollection<Feature> Features, ICollection<CoreEffect> Effects);

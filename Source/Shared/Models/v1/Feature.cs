namespace BlazorApp1.Shared.Models.v1;

public record Feature(Guid Id, string Name, ICollection<Character> Characters, ICollection<Effect> Effects);

namespace BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

[Key(nameof(Id))]
public record Feature
{
	public Guid Id { get; init; }
	public string? Name { get; set; }

	public ICollection<Character> Characters { get; set; } = new List<Character>();
	public ICollection<Effect> Effects { get; set; } = new List<Effect>();
}

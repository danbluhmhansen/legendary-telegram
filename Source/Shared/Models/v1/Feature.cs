namespace BlazorApp1.Shared.Models.v1;

public record Feature
{
	public Guid Id { get; init; }
	public string? Name { get; set; }

	public ICollection<Character> Characters { get; set; } = new List<Character>();
	public ICollection<Effect> Effects { get; set; } = new List<Effect>();
}

namespace BlazorApp1.Server.Entities;

public record Feature
{
	public Feature()
	{
		this.Effects = new List<Effect>();
	}

	public Feature(Guid id, string name, ICollection<Effect> effects)
	{
		this.Id = id;
		this.Name = name;
		this.Effects = effects;
	}

	public Guid Id { get; init; }
	public string? Name { get; set; }
	public virtual ICollection<Effect>? Effects { get; init; }
}

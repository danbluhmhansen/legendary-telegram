namespace BlazorApp1.Server.Entities;

public record Character
{
	public Character()
	{
		this.Features = new List<Feature>();
	}

	public Character(Guid id, string name, ICollection<Feature> features)
	{
		this.Id = id;
		this.Name = name;
		this.Features = features;
	}

	public Guid Id { get; init; }
	public string? Name { get; set; }
	public virtual ICollection<Feature>? Features { get; init; }
}

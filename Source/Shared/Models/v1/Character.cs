namespace BlazorApp1.Shared.Models.v1;

public record Character
{
	public Character()
	{
		this.Features = new List<Feature>();
		this.Effects = new List<CoreEffect>();
	}

	public Guid Id { get; set; }
	public string? Name { get; set; }

	public ICollection<Feature>? Features { get; set; }
	public ICollection<CoreEffect>? Effects { get; set; }
}

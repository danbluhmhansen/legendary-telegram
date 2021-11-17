namespace BlazorApp1.Shared.Models.v1;

public record Character
{
	public Guid Id { get; set; }
	public string? Name { get; set; }

	public ICollection<Feature>? Features { get; set; }
	public ICollection<CoreEffect>? Effects { get; set; }
}

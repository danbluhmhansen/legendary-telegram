namespace BlazorApp1.Shared.Models.v1;

using Microsoft.OData.Client;

[Key(nameof(Id))]
public record Character
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Feature> Features { get; set; } = new List<Feature>();
    public ICollection<CoreEffect> Effects { get; set; } = new List<CoreEffect>();
}

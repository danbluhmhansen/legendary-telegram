namespace BlazorApp1.Server.Entities;

public record Character
{
    public Character(Guid id, string name, ICollection<Feature> features, ICollection<CoreEffect> effects)
    {
        this.Id = id;
        this.Name = name;
        this.Features = features;
        this.Effects = effects;
    }
    public Character(Guid id, string name) : this(id, name, new List<Feature>(), new List<CoreEffect>()) { }

    public Character() { }

    public Guid Id { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<Feature>? Features { get; set; }
    public virtual ICollection<CoreEffect>? Effects { get; set; }
}

namespace BlazorApp1.Server.Entities;

public record Feature
{
    public Feature(Guid id, string name, ICollection<Character> characters, ICollection<Effect> effects)
    {
        this.Id = id;
        this.Name = name;
        this.Characters = characters;
        this.Effects = effects;
    }

    public Feature(Guid id, string name) : this(id, name, new List<Character>(), new List<Effect>()) { }

    public Feature() { }

    public Guid Id { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<Character>? Characters { get; set; }
    public virtual ICollection<Effect>? Effects { get; set; }
}

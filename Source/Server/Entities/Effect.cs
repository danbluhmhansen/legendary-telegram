namespace BlazorApp1.Server.Entities;

using System.Text.Json;

public record Effect
{
	public Effect(Guid id, string name, string path, JsonElement rule, Guid featureId, Feature feature)
	{
		this.Id = id;
		this.Name = name;
		this.Path = path;
		this.Rule = rule;
		this.FeatureId = featureId;
		this.Feature = feature;
	}
	public Effect(Guid id, string name, string path, JsonElement rule, Guid featureId)
		: this(id, name, path, rule, featureId, new Feature()) { }

	public Effect() { }

	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? Path { get; set; }
	public JsonElement Rule { get; set; }

	public Guid FeatureId { get; set; }
	public virtual Feature? Feature { get; set; }
}

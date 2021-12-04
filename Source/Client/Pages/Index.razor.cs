namespace BlazorApp1.Client.Pages;

using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;

public partial class Index : ComponentBase
{
	[Inject] private ILogger<Index>? Logger { get; init; }

	private IEnumerable<KeyValuePair<string, JsonNode?>> tree = new JsonObject
	{
		{
			"+",
			new JsonArray(
				new JsonObject { { "var", "Strength" } },
				2
			)
		}
	};

	protected override void OnParametersSet()
	{
		this.Logger?.LogInformation(((JsonObject)tree).ToJsonString());
		base.OnParametersSet();
	}

	private bool HasChildNodes(KeyValuePair<string, JsonNode?> parent)
	{
		return parent.Value switch
		{
			JsonObject obj => obj.Count > 0,
			JsonArray arr => arr.Count > 0,
			_ => false,
		};
	}

	private IEnumerable<KeyValuePair<string, JsonNode?>> GetChildNodes(KeyValuePair<string, JsonNode?> parent)
	{
		// this.Logger?.LogInformation(JsonSerializer.Serialize(parent));
		return parent.Value switch
		{
			JsonObject obj => obj,
			JsonArray arr => arr.Select((JsonNode? node) =>
			{
				// this.Logger?.LogInformation(node.GetType().Name);
				// this.Logger?.LogInformation(JsonSerializer.Serialize(node));
				if (node is JsonObject obj)
				{
					this.Logger?.LogInformation(JsonSerializer.Serialize(obj));
				}
				return new KeyValuePair<string, JsonNode?>(string.Empty, node);
			}),
			_ => Enumerable.Empty<KeyValuePair<string, JsonNode?>>(),
		};
	}
}

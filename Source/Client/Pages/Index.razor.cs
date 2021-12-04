namespace BlazorApp1.Client.Pages;

using System.Linq;
using System.Text.Json.Nodes;

using BlazorApp1.Shared.Extensions;

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
		return parent.Value switch
		{
			JsonObject obj => obj,
			JsonArray arr => arr.OfType<JsonObject>().SelectMany()
				.Concat(arr.OfType<JsonArray>().SelectMany().Select((JsonNode? node) =>
					new KeyValuePair<string, JsonNode?>(string.Empty, node)))
				.Concat(arr.OfType<JsonValue>().Select((JsonValue val) =>
					new KeyValuePair<string, JsonNode?>(string.Empty, val))),
			_ => Enumerable.Empty<KeyValuePair<string, JsonNode?>>(),
		};
	}
}

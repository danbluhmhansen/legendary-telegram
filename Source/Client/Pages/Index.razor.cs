namespace BlazorApp1.Client.Pages;

using System.Linq;
using System.Text.Json.Nodes;

using BlazorApp1.Shared.Extensions;

using Blazorise;

using Microsoft.AspNetCore.Components;

public partial class Index : ComponentBase
{
	[Inject] private ILogger<Index>? Logger { get; init; }

	private ICollection<KeyValuePair<string, JsonNode?>> tree = new JsonObject
	{
		{
			"+",
			new JsonArray(
				new JsonObject { { "var", "Strength" } },
				2
			)
		}
	};

	private KeyValuePair<string, JsonNode?> selected;

	private bool modalVisible;
	private string? selectedOperation;
	private string? selectedValue;

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

	private void Clear() => this.selected = new KeyValuePair<string, JsonNode?>();

	private void Remove()
	{
		if (this.selected.Value is null)
			return;

		if (this.selected.Value.Parent is JsonObject obj)
			obj.Remove(this.selected.Key);
		else if (this.selected.Value.Parent is JsonArray arr)
			arr.Remove(this.selected.Value);
	}

	private Task Opening(ModalOpeningEventArgs args)
	{
		if (this.selected.Value is JsonValue)
			this.selectedValue = this.selected.Value.ToJsonString().Trim('"');

		return Task.CompletedTask;
	}

	private void Submit()
	{
		if (!string.IsNullOrWhiteSpace(this.selectedOperation))
		{
			switch (this.selected.Value)
			{
				case JsonObject obj:
					obj[this.selectedOperation] = this.selectedOperation switch
					{
						"var" => JsonValue.Create("placeholder"),
						_ => new JsonArray(),
					};
					break;
				case JsonArray arr:
					arr.Add(this.selectedOperation switch
					{
						"var" => new JsonObject { { "var", JsonValue.Create("placeholder") } },
						"lit" => JsonValue.Create("placeholder"),
						_ => new JsonObject { { this.selectedOperation, new JsonArray() } },
					});
					break;
				default:
					this.tree.Add(new KeyValuePair<string, JsonNode?>(this.selectedOperation, new JsonArray()));
					break;
			}

			this.selectedOperation = default;
		}
		else
		{
			JsonValue? jsonValue = decimal.TryParse(this.selectedValue, out decimal number)
				? JsonValue.Create(number)
				: JsonValue.Create(this.selectedValue);
			switch (this.selected.Value?.Parent)
			{
				case JsonObject obj:
					obj[this.selected.Key] = jsonValue;
					break;
				case JsonArray arr:
					int index = arr.IndexOf(this.selected.Value);
					arr.RemoveAt(index);
					arr.Insert(index, jsonValue);
					break;
			}

			Clear();
			this.selectedValue = default;
		}

		this.modalVisible = false;
	}
}

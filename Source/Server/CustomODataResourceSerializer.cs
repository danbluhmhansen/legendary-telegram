namespace BlazorApp1.Server;

using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.OData;
using Microsoft.OData.Edm;

public class CustomODataResourceSerializer : ODataResourceSerializer
{
	public CustomODataResourceSerializer(IODataSerializerProvider serializerProvider) : base(serializerProvider) { }

	public override async Task WriteObjectInlineAsync(
		object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
	{
		switch (graph)
		{
			case JsonObject obj:
				ODataResource resource = new()
				{
					Properties = obj.Select((KeyValuePair<string, JsonNode?> node) => new ODataProperty
					{
						Name = node.Key,
						Value = JsonNodeToOdataValue(node.Value),
					}),
				};
				await writer.WriteStartAsync(resource);
				await writer.WriteEndAsync();
				break;
			case JsonArray arr:
				break;
			case JsonValue val:
				break;
			default:
				await base.WriteObjectInlineAsync(graph, expectedType, writer, writeContext);
				break;
		}
	}

	private ODataResourceValue JsonObjectToODataResource(JsonObject jsonObject)
	{
		return new ODataResourceValue
		{
			TypeName = nameof(JsonObject),
			Properties = jsonObject.Select((KeyValuePair<string, JsonNode?> node) => new ODataProperty
			{
				Name = node.Key,
				Value = JsonNodeToOdataValue(node.Value),
			}),
		};
	}

	private ODataCollectionValue JsonArrayToODataCollection(JsonArray jsonArray)
	{
		return new ODataCollectionValue
		{
			Items = jsonArray.Select((JsonNode? node) => JsonNodeToOdataValue(node)),
		};
	}

	private object? JsonNodeToOdataValue(JsonNode? jsonNode)
	{
		return jsonNode switch
		{
			JsonValue val when val.TryGetValue(out int number) => number,
			JsonValue val when val.TryGetValue(out decimal number) => number,
			JsonValue val when val.TryGetValue(out bool boolean) => boolean,
			JsonValue val when val.TryGetValue(out string? text) => text,
			JsonObject obj => JsonObjectToODataResource(obj),
			JsonArray arr => JsonArrayToODataCollection(arr),
			_ => null,
		};
	}
}

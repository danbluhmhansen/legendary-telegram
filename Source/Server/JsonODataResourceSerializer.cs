namespace BlazorApp1.Server;

using System.Text.Json;
using System.Text.Json.Nodes;

using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.OData;
using Microsoft.OData.Edm;

public class JsonODataResourceSerializer : ODataResourceSerializer
{
    public JsonODataResourceSerializer(IODataSerializerProvider serializerProvider) : base(serializerProvider) { }

    public override async Task WriteObjectInlineAsync(
        object graph,
        IEdmTypeReference expectedType,
        ODataWriter writer,
        ODataSerializerContext writeContext)
    {
        if (graph is null)
            return;

        if (graph is JsonObject obj)
        {
            await writer.WriteStartAsync(new ODataResource
            {
                Properties = obj.Select((KeyValuePair<string, JsonNode?> node) => new ODataProperty
                {
                    Name = node.Key,
                    Value = new ODataUntypedValue { RawValue = node.Value?.ToJsonString() },
                }),
            });
            await writer.WriteEndAsync();
        }
        else if (graph is KeyValuePair<string, JsonNode?> node)
        {
            await writer.WriteStartAsync(new ODataResource
            {
                Properties = new[]
                {
                    new ODataProperty
                    {
                        Name = node.Key,
                        Value = new ODataUntypedValue { RawValue = node.Value?.ToJsonString() },
                    }
                }
            });
            await writer.WriteEndAsync();
        }
        else if (graph is JsonElement element && element.ValueKind is JsonValueKind.Object)
        {
            await writer.WriteStartAsync(new ODataResource
            {
                Properties = element.EnumerateObject().Select((JsonProperty prop) => new ODataProperty
                {
                    Name = prop.Name,
                    Value = new ODataUntypedValue { RawValue = prop.Value.GetRawText() },
                }),
            });
            await writer.WriteEndAsync();
        }
        else
        {
            await base.WriteObjectInlineAsync(graph, expectedType, writer, writeContext);
        }
    }
}

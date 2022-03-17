namespace BlazorApp1.Server;

using System.Text.Json;
using System.Text.Json.Nodes;

using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.AspNetCore.OData.Formatter.Wrapper;
using Microsoft.OData;
using Microsoft.OData.Edm;

public class JsonODataResourceDeserializer : ODataResourceDeserializer
{
    public JsonODataResourceDeserializer(IODataDeserializerProvider deserializerProvider) : base(deserializerProvider)
    { }

    public override void ApplyStructuralProperty(
        object resource,
        ODataProperty structuralProperty,
        IEdmStructuredTypeReference structuredType,
        ODataDeserializerContext readContext)
    {
        if (structuredType == null)
        {
            throw new ArgumentNullException(nameof(structuredType));
        }
        else if (structuredType.Definition.FullTypeName() == "System.Text.Json.JsonElement"
           || (structuredType.Definition.FullTypeName() == "System.Text.Json.JsonNode" && resource is JsonObject)
           || structuredType.Definition.FullTypeName() == "System.Text.Json.JsonObject")
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (structuralProperty == null)
                throw new ArgumentNullException(nameof(structuralProperty));

            if (readContext == null)
                throw new ArgumentNullException(nameof(readContext));

            JsonNode? value = JsonNode.Parse(JsonSerializer.Serialize(structuralProperty.Value));

            JsonObject? obj = resource as JsonObject;
            obj?.Add(structuralProperty.Name, value);
        }
        else
        {
            base.ApplyStructuralProperty(resource, structuralProperty, structuredType, readContext);
        }
    }

    public override object CreateResourceInstance(
        IEdmStructuredTypeReference structuredType,
        ODataDeserializerContext readContext)
    {
        if (structuredType == null)
            throw new ArgumentNullException(nameof(structuredType));

        if (structuredType.Definition.FullTypeName() != "System.Text.Json.JsonElement")
            return base.CreateResourceInstance(structuredType, readContext);

        return new JsonObject();
    }

    public override object ReadResource(ODataResourceWrapper resourceWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
    {
        object resource = base.ReadResource(resourceWrapper, structuredType, readContext);

        if (structuredType.Definition.FullTypeName() == "System.Text.Json.JsonElement")
            return ((JsonObject)resource).Deserialize<JsonElement>();

        return resource;
    }
}

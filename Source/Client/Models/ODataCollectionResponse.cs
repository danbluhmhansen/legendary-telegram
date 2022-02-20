namespace BlazorApp1.Client.Models;

using System.Text.Json.Serialization;

public record ODataCollectionResponse<T>(
    [property: JsonPropertyName("@odata.context")] Uri Context,
    [property: JsonPropertyName("@odata.count")] int? Count,
    IEnumerable<T> Value);

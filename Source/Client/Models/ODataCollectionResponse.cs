namespace LegendaryTelegram.Client.Models;

using System.Collections;
using System.Text.Json.Serialization;

public class ODataCollectionResponse<T> : IEnumerable<T>
{
    public ODataCollectionResponse(IEnumerable<T> value, Uri context, int count = 0)
    {
       this.items = value; 
       this.Context = context;
       this.Count = count;
    }

    [JsonPropertyName("@odata.context")]
    public Uri Context { get; }
    [JsonPropertyName("@odata.count")]
    public int Count { get; }

    private readonly IEnumerable<T> items;

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
}


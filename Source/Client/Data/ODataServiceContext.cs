using LegendaryTelegram.Client.Models;

using Microsoft.OData.Client;

namespace LegendaryTelegram.Client.Data;

public class ODataServiceContext : DataServiceContext
{
    public ODataServiceContext() : base(new Uri("https://localhost:7000/api"))
    {
        this.HttpRequestTransportMode = HttpRequestTransportMode.HttpClient;

        this.Characters = base.CreateQuery<Character>("Characters");
    }

    public DataServiceQuery<Character> Characters { get; init; }
}


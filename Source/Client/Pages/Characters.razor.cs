using System.Net.Http.Json;
using System.Web;

using LegendaryTelegram.Client.Components;
using LegendaryTelegram.Client.Data;
using LegendaryTelegram.Client.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.OData.Client;

namespace LegendaryTelegram.Client.Pages;

public partial class Characters : ComponentBase
{
    [CascadingParameter(Name = "ErrorComponent")]
    private IErrorComponent? ErrorComponent { get; init; }

    [Inject] private ILogger<Characters>? Logger { get; init; }
    [Inject] private HttpClient? HttpClient { get; init; }
    [Inject] private ODataServiceContext? ServiceContext { get; init; }

    private GridItemsProvider<Character>? charactersProvider;

    protected override Task OnInitializedAsync()
    {
        if (ServiceContext is null) throw new ArgumentNullException(nameof(ServiceContext));
        if (HttpClient is null) throw new ArgumentNullException(nameof(ServiceContext));

        charactersProvider = async req =>
        {
            ICollection<Character> characters = new List<Character>();
            int count = 0;

            try
            {
                var charactersQuery = (DataServiceQuery<Character>)req.ApplySorting(ServiceContext.Characters);

                UriBuilder builder = new(charactersQuery.ToString());
                var query = HttpUtility.ParseQueryString(builder.Query);
                query.Set("api-version", "1.0");
                builder.Query = query.ToString();

                var response = await HttpClient.GetFromJsonAsync<ODataCollectionResponse<Character>>(builder.Uri);
                if (response is not null) characters = response.ToList();
                if (response?.Count is not null) count = response.Count;
            }
            catch (Exception)
            {
                ErrorComponent!.ShowError(new Error("Error", "Error while getting data, try reloading."));
            }

            return GridItemsProviderResult.From(characters, count);
        };

        return Task.CompletedTask;
    }
}


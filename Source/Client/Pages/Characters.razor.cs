namespace BlazorApp1.Client.Pages;

using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Web;

using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Models.v1;

using Blazorise;
using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

public partial class Characters : ComponentBase
{
	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IConfiguration? Configuration { get; init; }
	[Inject] private NavigationManager? Navigation { get; init; }

	private ICollection<Character>? data;
	private int? count;

	private async Task OnReadData(DataGridReadDataEventArgs<Character> args)
	{
		if (this.Client is null)
			return;

		IEnumerable<string> filters = args.Columns
			.Where(column => column.SearchValue is not null)
			.Select(column => column.ColumnType switch
			{
				DataGridColumnType.Text => $"contains({column.Field},'{column.SearchValue}')",
				_ => "",
			});

		IEnumerable<string> orderBy = args.Columns
			.Where(column => column.SortDirection is not SortDirection.None)
			.OrderBy(column => column.SortIndex)
			.Select(column =>
				column.SortDirection is SortDirection.Ascending ? column.Field : $"{column.Field} desc");

		UriBuilder uriBuilder = new($"{this.Configuration.GetValue<string>("ServerUrl")}v1/characters");

		NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

		if (filters.Any())
			query["$filter"] = string.Join(", ", filters);
		if (orderBy.Any())
			query["$orderby"] = string.Join(", ", orderBy);

		switch (args.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query["$skip"] = ((args.Page - 1) * args.PageSize).ToString();
				query["$top"] = args.PageSize.ToString();
				break;
			case DataGridReadDataMode.Virtualize:
				query["$skip"] = args.VirtualizeOffset.ToString();
				query["$top"] = args.VirtualizeCount.ToString();
				break;
		}

		query["$count"] = "true";

		uriBuilder.Query = query.ToString();

		ODataResponse<Character>? response = await this.Client.GetFromJsonAsync<ODataResponse<Character>>(
			uriBuilder.Uri, args.CancellationToken);

		if (response is null)
			return;

		this.data = response.Value.ToList();
		this.count = response.Count;

		StateHasChanged();
	}

	private void OnSelectRow(Character args) => this.Navigation?.NavigateTo($"/Characters/{args.Id}");
}

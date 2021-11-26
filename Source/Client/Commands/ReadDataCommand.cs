namespace BlazorApp1.Client.Commands;

using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Web;

using BlazorApp1.Client.Models;
using Blazorise;
using Blazorise.DataGrid;

public class ReadDataCommand
{
	private readonly HttpClient client;
	private readonly IConfiguration configuration;
	private readonly ILogger<ReadDataCommand> logger;

	public ReadDataCommand(HttpClient client, IConfiguration configuration, ILogger<ReadDataCommand> logger)
	{
		this.client = client;
		this.configuration = configuration;
		this.logger = logger;
	}

	public async Task<ODataCollectionResponse<T>?> ExecuteAsync<T>(DataGridReadDataEventArgs<T> eventArgs, string path,
		string? expand = default)
	{
		IEnumerable<string> filters = eventArgs.Columns
			.Where(column => column.SearchValue is not null)
			.Select(column => column.ColumnType switch
			{
				DataGridColumnType.Text => $"contains({column.Field},'{column.SearchValue}')",
				_ => "",
			});

		IEnumerable<string> orderBy = eventArgs.Columns
			.Where(column => column.SortDirection is not SortDirection.None)
			.OrderBy(column => column.SortIndex)
			.Select(column =>
				column.SortDirection is SortDirection.Ascending ? column.Field : $"{column.Field} desc");

		UriBuilder uriBuilder = new(this.configuration.GetValue<string>("ServerUrl") + path);

		NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

		if (filters.Any())
			query["$filter"] = string.Join(", ", filters);
		if (orderBy.Any())
			query["$orderby"] = string.Join(", ", orderBy);

		switch (eventArgs.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query["$skip"] = ((eventArgs.Page - 1) * eventArgs.PageSize).ToString();
				query["$top"] = eventArgs.PageSize.ToString();
				break;
			case DataGridReadDataMode.Virtualize:
				query["$skip"] = eventArgs.VirtualizeOffset.ToString();
				query["$top"] = eventArgs.VirtualizeCount.ToString();
				break;
		}

		query["$count"] = "true";

		if (!string.IsNullOrWhiteSpace(expand))
			query["$expand"] = expand;

		uriBuilder.Query = query.ToString();

		return await this.client
			.GetFromJsonAsync<ODataCollectionResponse<T>>(uriBuilder.Uri, eventArgs.CancellationToken);
	}
}

namespace BlazorApp1.Client.Commands;

using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Web;

using BlazorApp1.Client.Configuration;
using BlazorApp1.Client.Models;

using Blazorise;
using Blazorise.DataGrid;

using Microsoft.Extensions.Options;

public class ReadDataCommand
{
	private readonly HttpClient client;
	private readonly IOptions<ServerOptions> serverOptions;
	private readonly ILogger<ReadDataCommand> logger;

	public ReadDataCommand(HttpClient client, IOptions<ServerOptions> serverOptions, ILogger<ReadDataCommand> logger)
	{
		this.client = client;
		this.logger = logger;
		this.serverOptions = serverOptions;
	}

	public async Task<ODataCollectionResponse<T>?> ExecuteAsync<T>(DataGridReadDataEventArgs<T> eventArgs, string path,
		string? expand = default, IEnumerable<string>? filters = default)
	{
		IEnumerable<string> columnFilters = eventArgs.Columns
			.Where(column => column.SearchValue is not null)
			.Select(column => column.ColumnType switch
			{
				DataGridColumnType.Text => $"contains({column.Field},'{column.SearchValue}')",
				_ => "",
			});

		if (filters is not null)
			columnFilters = columnFilters.Concat(filters);

		IEnumerable<string> orderBy = eventArgs.Columns
			.Where(column => column.SortDirection is not SortDirection.None)
			.OrderBy(column => column.SortIndex)
			.Select(column =>
				column.SortDirection is SortDirection.Ascending ? column.Field : $"{column.Field} desc");

		UriBuilder uriBuilder = new(this.serverOptions.Value.Route + path);

		NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

		if (columnFilters.Any())
			query["$filter"] = string.Join(", ", columnFilters);
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

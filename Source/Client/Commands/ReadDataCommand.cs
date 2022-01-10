namespace BlazorApp1.Client.Commands;

using System.Net.Http.Json;

using BlazorApp1.Client.Data;
using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Extensions;

using Blazorise;
using Blazorise.DataGrid;

using Microsoft.OData.Client;

public class ReadDataCommand
{
	private readonly ODataServiceContext context;
	private readonly HttpClient client;
	private readonly ILogger<ReadDataCommand> logger;

	public ReadDataCommand(ODataServiceContext context, HttpClient client, ILogger<ReadDataCommand> logger)
	{
		this.client = client;
		this.logger = logger;
		this.context = context;
	}

	public async Task<ODataCollectionResponse<T>?> ExecuteAsync<T>(
		DataGridReadDataEventArgs<T> eventArgs,
		string path,
		string? expand = default,
		IEnumerable<string>? filters = default)
	{
		DataServiceQuery<T> query = this.context.CreateQuery<T>(path);

		switch (eventArgs.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query = (DataServiceQuery<T>)query.Skip((eventArgs.Page - 1) * eventArgs.PageSize).Take(eventArgs.PageSize);
				break;
			case DataGridReadDataMode.Virtualize:
				query = (DataServiceQuery<T>)query.Skip(eventArgs.VirtualizeOffset).Take(eventArgs.VirtualizeCount);
				break;
		}

		IOrderedEnumerable<DataGridColumnInfo> orderColumns = eventArgs.Columns
			.Where((DataGridColumnInfo column) => column.SortDirection is not SortDirection.None)
			.OrderBy((DataGridColumnInfo column) => column.SortIndex);
		if (orderColumns.Any())
		{
			DataGridColumnInfo firstColumn = orderColumns.First();
			IOrderedQueryable<T> orderedQuery = firstColumn.SortDirection is SortDirection.Ascending
				? query.OrderBy(firstColumn.SortField)
				: query.OrderByDescending(firstColumn.SortField);

			foreach (DataGridColumnInfo column in orderColumns.Skip(1))
			{
				orderedQuery = firstColumn.SortDirection is SortDirection.Ascending
					? query.OrderBy(firstColumn.SortField)
					: query.OrderByDescending(firstColumn.SortField);
			}

			query = (DataServiceQuery<T>)orderedQuery;
		}

		IEnumerable<string> columnFilters = eventArgs.Columns
			.Where(column => column.SearchValue is not null)
			.Select(column => column.ColumnType switch
			{
				DataGridColumnType.Text => $"contains({column.Field},'{column.SearchValue}')",
				_ => "",
			});

		if (filters is not null)
			columnFilters = columnFilters.Concat(filters);

		return await this.client
			.GetFromJsonAsync<ODataCollectionResponse<T>>(query.RequestUri, eventArgs.CancellationToken);
	}
}

namespace BlazorApp1.Client.Pages.Characters;

using BlazorApp1.Client.Data;
using BlazorApp1.Shared.Extensions;
using BlazorApp1.Shared.Models.v1;

using Blazorise;
using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.OData.Client;

public partial class Characters : ComponentBase
{
	[Inject] private ODataServiceContext? Context { get; init; }
	[Inject] private NavigationManager? Navigation { get; init; }
	[Inject] private ILogger<Characters> Logger { get; init; }

	private ICollection<Character> data = new List<Character>();
	private int? count;

	private async Task OnReadData(DataGridReadDataEventArgs<Character> args)
	{
		if (this.Context is null)
			return;

		IQueryable<Character> query = this.Context.Characters.IncludeCount();

		switch (args.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query = query.Skip((args.Page - 1) * args.PageSize).Take(args.PageSize);
				break;
			case DataGridReadDataMode.Virtualize:
				query = query.Skip(args.VirtualizeOffset).Take(args.VirtualizeCount);
				break;
		}

		IOrderedEnumerable<DataGridColumnInfo> orderColumns = args.Columns
			.Where((DataGridColumnInfo column) => column.SortDirection is not SortDirection.None)
			.OrderBy((DataGridColumnInfo column) => column.SortIndex);
		if (orderColumns.Any())
		{
			DataGridColumnInfo firstColumn = orderColumns.First();
			IOrderedQueryable<Character> orderedQuery = firstColumn.SortDirection is SortDirection.Ascending
				? query.OrderBy(firstColumn.SortField)
				: query.OrderByDescending(firstColumn.SortField);

			foreach (DataGridColumnInfo column in orderColumns.Skip(1))
			{
				orderedQuery = firstColumn.SortDirection is SortDirection.Ascending
					? query.OrderBy(firstColumn.SortField)
					: query.OrderByDescending(firstColumn.SortField);
			}

			query = orderedQuery;
		}

		if (await ((DataServiceQuery<Character>)query).ExecuteAsync(args.CancellationToken)
			is not QueryOperationResponse<Character> response)
			return;

		this.data = response.ToList();
		this.count = (int?)response.Count;
	}

	private void OnSelectRow(Character args) => this.Navigation?.NavigateTo($"/Characters/{args.Id}");
}

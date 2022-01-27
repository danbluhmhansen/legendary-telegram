namespace BlazorApp1.Client.Pages.Features;

using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Data;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.OData.Client;

public partial class Features : ComponentBase
{
	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private ODataServiceContext? ServiceContext { get; init; }
	[Inject] private ILogger<Features>? Logger { get; init; }

	private ICollection<Feature> data = new List<Feature>();
	private int? count;

	private DataGrid<Feature>? dataGrid;

	private async Task OnReadData(DataGridReadDataEventArgs<Feature> args)
	{
		if (this.ReadDataCommand is null)
			return;

		DataServiceQuery<Feature> query = this.ReadDataCommand.Execute(args, "Features")
			.Expand(nameof(Feature.Effects));

		if (await query.ExecuteAsync(args.CancellationToken) is not QueryOperationResponse<Feature> response)
			return;

		this.data = response.ToList();
		this.count = (int)response.Count;

		StateHasChanged();
	}

	private async Task OnRowInserted(SavedRowItem<Feature, Dictionary<string, object>> args)
	{
		if (this.ServiceContext is null || args.Item is null)
			return;

		this.ServiceContext.AddObject("Features", args.Item);
		await this.ServiceContext.SaveChangesAsync();
	}

	private async Task OnRowUpdated(SavedRowItem<Feature, Dictionary<string, object>> args)
	{
		if (this.ServiceContext is null || args.Item is null)
			return;

		this.ServiceContext.AttachTo("Features", args.Item);
		this.ServiceContext.UpdateObject(args.Item);
		await this.ServiceContext.SaveChangesAsync();
	}

	private async Task OnRowRemoving(CancellableRowChange<Feature> args)
	{
		if (this.ServiceContext is null || args.Item is null)
			return;

		this.ServiceContext.AttachTo("Features", args.Item);
		this.ServiceContext.DeleteObject(args.Item);
		await this.ServiceContext.SaveChangesAsync();
	}
}

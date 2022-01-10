namespace BlazorApp1.Client.Pages.Features;

using System.Net.Http.Json;
using System.Text.Json;

using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Configuration;
using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

public partial class Features : ComponentBase
{
	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IOptions<JsonSerializerOptions>? JsonSerializerOptions { get; init; }
	[Inject] private IOptions<ServerOptions>? ServerOptions { get; init; }
	[Inject] private ILogger<Features>? Logger { get; init; }

	private ICollection<Feature> data = new List<Feature>();
	private int? count;

	private DataGrid<Feature>? dataGrid;

	private async Task OnReadData(DataGridReadDataEventArgs<Feature> args)
	{
		if (this.ReadDataCommand is null)
			return;

		ODataCollectionResponse<Feature>? response = await this.ReadDataCommand.ExecuteAsync(
			args, "Features", "Effects");

		if (response is null)
			return;

		this.data = response.Value.ToList();
		this.count = response.Count;

		StateHasChanged();
	}

	private async Task OnRowInserting(CancellableRowChange<Feature, Dictionary<string, object>> args)
	{
		if (this.Client is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.Client.PostAsJsonAsync(
			$"{this.ServerOptions.Value.Route}v1/Features", args.Values,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnRowUpdating(CancellableRowChange<Feature, Dictionary<string, object>> args)
	{
		if (this.Client is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.Client.PutAsJsonAsync(
			$"{this.ServerOptions.Value.Route}v1/Features", args.Item,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnRowRemoving(CancellableRowChange<Feature> args)
	{
		if (this.Client is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.Client.DeleteAsJsonAsync(
			$"{this.ServerOptions.Value.Route}v1/Features", args.Item,
			this.JsonSerializerOptions?.Value);
	}
}

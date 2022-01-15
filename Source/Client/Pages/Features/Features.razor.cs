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
using Microsoft.OData.Client;

public partial class Features : ComponentBase
{
	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private HttpClient? HttpClient { get; init; }
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

		DataServiceQuery<Feature> query = this.ReadDataCommand.Execute(args, "v1/Features")
			.Expand(nameof(Feature.Effects));

		if (await query.ExecuteAsync(args.CancellationToken) is not QueryOperationResponse<Feature> response)
			return;

		this.data = response.ToList();
		this.count = (int)response.Count;

		StateHasChanged();
	}

	private async Task OnRowInserting(CancellableRowChange<Feature, Dictionary<string, object>> args)
	{
		if (this.HttpClient is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.HttpClient.PostAsJsonAsync(
			$"{this.ServerOptions.Value.Route}v1/Features", args.Values,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnRowUpdating(CancellableRowChange<Feature, Dictionary<string, object>> args)
	{
		if (this.HttpClient is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.HttpClient.PutAsJsonAsync(
			$"{this.ServerOptions.Value.Route}v1/Features", args.Item,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnRowRemoving(CancellableRowChange<Feature> args)
	{
		if (this.HttpClient is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.HttpClient.DeleteAsJsonAsync(
			$"{this.ServerOptions.Value.Route}v1/Features", args.Item,
			this.JsonSerializerOptions?.Value);
	}
}

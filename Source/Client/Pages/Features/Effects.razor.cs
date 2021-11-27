namespace BlazorApp1.Client.Pages.Features;

using System.Text.Json;

using BlazorApp1.Client.Configuration;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

public partial class Effects : ComponentBase
{
	[Parameter] public Feature? Feature { get; init; }

	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IOptions<JsonSerializerOptions>? JsonSerializerOptions { get; init; }
	[Inject] private IOptions<ServerOptions>? ServerOptions { get; init; }

	private async Task OnRowRemoving(CancellableRowChange<Effect> args)
	{
		if (this.Client is null || this.ServerOptions is null || args.Item is null)
			return;

		await this.Client.DeleteAsJsonAsync($"{this.ServerOptions.Value.Route}v1/Effects", args.Item,
			this.JsonSerializerOptions?.Value);
	}
}

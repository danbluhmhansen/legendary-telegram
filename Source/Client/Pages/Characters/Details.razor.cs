namespace BlazorApp1.Client.Pages.Characters;

using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Configuration;
using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

public partial class Details : ComponentBase
{
	[Parameter] public Guid Id { get; init; }

	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private ComputeCharacterCommand? ComputeCharacterCommand { get; init; }
	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IOptions<JsonSerializerOptions>? JsonSerializerOptions { get; init; }
	[Inject] private IOptions<ServerOptions>? ServerOptions { get; init; }
	[Inject] private ILogger<Details>? Logger { get; init; }

	private Character? character;
	private JsonElement json;

	private IEnumerable<Feature>? features;
	private int? featuresCount;
	private List<Feature> selectedFeatures = new();

	protected override async Task OnParametersSetAsync()
	{
		if (this.Client is null || this.ServerOptions is null)
			return;

		this.character = await this.Client.GetFromJsonAsync<Character>(
			$"{this.ServerOptions.Value.Route}v1/Characters/{this.Id}?$expand=Features($expand=Effects),Effects");

		if (this.ComputeCharacterCommand is not null && this.character is not null)
			this.json = this.ComputeCharacterCommand.Execute(this.character);
	}

	private async Task OnReadFeatures(DataGridReadDataEventArgs<Feature> args)
	{
		if (this.ReadDataCommand is null)
			return;

		ODataCollectionResponse<Feature>? response = await this.ReadDataCommand.ExecuteAsync(args, "v1/Features",
			filters: new[] { $"Characters/all(c:c/Id ne {this.Id})" });

		if (response is null)
			return;

		this.features = this.character?.Features?.Any() == true
			? response.Value.Except(this.character.Features).ToList()
			: response.Value.ToList();
		this.featuresCount = response.Count;

		StateHasChanged();
	}

	private async Task OnFeatureInserting()
	{
		if (this.Client is null || this.ServerOptions is null || this.selectedFeatures?.Any() != true)
			return;

		IEnumerable<ODataRequest> requests = this.selectedFeatures
			.Select((Feature feature, int i) => new ODataRequest(i.ToString(), "POST",
				new Uri($"{this.ServerOptions.Value.Route}v1/CharacterFeatures"),
				new Dictionary<string, string>() { { "Content-Type", "application/json" } },
				new CharacterFeature
				{
					CharacterId = this.Id,
					FeatureId = feature.Id,
				}));

		await this.Client.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/$batch", new ODataBatchRequest(requests),
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnFeatureRemoving()
	{
		if (this.Client is null || this.ServerOptions is null || this.selectedFeatures?.Any() != true)
			return;

		IEnumerable<ODataRequest> requests = this.selectedFeatures
			.Select((Feature feature, int i) => new ODataRequest(i.ToString(), "DELETE",
				new Uri($"{this.ServerOptions.Value.Route}v1/CharacterFeatures"),
				new Dictionary<string, string>() { { "Content-Type", "application/json" } },
				new CharacterFeature
				{
					CharacterId = this.Id,
					FeatureId = feature.Id,
				}));

		await this.Client.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/$batch", new ODataBatchRequest(requests),
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnEffectInserting(CancellableRowChange<CoreEffect, Dictionary<string, object>> args)
	{
		if (this.Client is null || this.ServerOptions is null)
			return;

		args.Values[nameof(CoreEffect.CharacterId)] = this.Id;

		await this.Client.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/CoreEffects", args.Values,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnEffectUpdating(CancellableRowChange<CoreEffect, Dictionary<string, object>> args)
	{
		if (this.Client is null || this.ServerOptions is null)
			return;

		await this.Client.PutAsJsonAsync($"{this.ServerOptions.Value.Route}v1/CoreEffects", args.Values,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnEffectRemoving(CancellableRowChange<CoreEffect> args)
	{
		if (this.Client is null || this.ServerOptions is null)
			return;

		await this.Client.DeleteAsJsonAsync($"{this.ServerOptions.Value.Route}v1/CoreEffects", args.Item,
			this.JsonSerializerOptions?.Value);
	}
}

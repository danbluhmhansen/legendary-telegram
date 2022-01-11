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
using Microsoft.OData.Client;

public partial class Details : ComponentBase
{
	[Parameter] public Guid Id { get; init; }

	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private ComputeCharacterCommand? ComputeCharacterCommand { get; init; }
	[Inject] private HttpClient? HttpClient { get; init; }
	[Inject] private IOptions<JsonSerializerOptions>? JsonSerializerOptions { get; init; }
	[Inject] private IOptions<ServerOptions>? ServerOptions { get; init; }
	[Inject] private ILogger<Details>? Logger { get; init; }

	private Character? character;
	private JsonElement json;

	private IEnumerable<Feature>? features;
	private int? featuresCount;
	private readonly List<Feature> selectedFeatures = new();

	private IEnumerable<JsonProperty> GetJsonProperties =>
		this.json.EnumerateObject()
			.Where(x => x.Value.ValueKind switch
			{
				JsonValueKind.String or JsonValueKind.Number => true,
				_ => false,
			});

	protected override async Task OnParametersSetAsync()
	{
		if (this.HttpClient is null || this.ServerOptions is null)
			return;

		this.character = await this.HttpClient.GetFromJsonAsync<Character>(
			$"{this.ServerOptions.Value.Route}v1/Characters/{this.Id}?$expand=Features($expand=Effects),Effects");

		if (this.ComputeCharacterCommand is not null && this.character is not null)
			this.json = this.ComputeCharacterCommand.Execute(this.character);
	}

	private async Task OnReadFeatures(DataGridReadDataEventArgs<Feature> args)
	{
		if (this.ReadDataCommand is null || this.HttpClient is null)
			return;

		DataServiceQuery<Feature> query = (DataServiceQuery<Feature>)this.ReadDataCommand.Execute(args, "Features")
			.Where((Feature feature) => feature.Characters.All((Character character) => this.Id == character.Id));

		ODataCollectionResponse<Feature>? response = await this.HttpClient
			.GetFromJsonAsync<ODataCollectionResponse<Feature>>(query.RequestUri, args.CancellationToken);

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
		if (this.HttpClient is null || this.ServerOptions is null || this.selectedFeatures?.Any() != true)
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

		await this.HttpClient.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/$batch", new ODataBatchRequest(requests),
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnFeatureRemoving()
	{
		if (this.HttpClient is null || this.ServerOptions is null || this.selectedFeatures?.Any() != true)
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

		await this.HttpClient.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/$batch", new ODataBatchRequest(requests),
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnEffectInserting(CancellableRowChange<CoreEffect, Dictionary<string, object>> args)
	{
		if (this.HttpClient is null || this.ServerOptions is null)
			return;

		args.Values[nameof(CoreEffect.CharacterId)] = this.Id;

		await this.HttpClient.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/CoreEffects", args.Values,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnEffectUpdating(CancellableRowChange<CoreEffect, Dictionary<string, object>> args)
	{
		if (this.HttpClient is null || this.ServerOptions is null)
			return;

		args.Values[nameof(args.Item.Id)] = args.Item.Id;
		args.Values[nameof(args.Item.CharacterId)] = args.Item.CharacterId;

		await this.HttpClient.PutAsJsonAsync($"{this.ServerOptions.Value.Route}v1/CoreEffects", args.Values,
			this.JsonSerializerOptions?.Value);
	}

	private async Task OnEffectRemoving(CancellableRowChange<CoreEffect> args)
	{
		if (this.HttpClient is null || this.ServerOptions is null)
			return;

		await this.HttpClient.DeleteAsJsonAsync($"{this.ServerOptions.Value.Route}v1/CoreEffects", args.Item,
			this.JsonSerializerOptions?.Value);
	}
}

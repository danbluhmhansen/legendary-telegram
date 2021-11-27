namespace BlazorApp1.Client.Pages.Characters;

using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Configuration;
using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Models.v1;

using Blazorise;
using Blazorise.DataGrid;

using Force.DeepCloner;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

public partial class Details : ComponentBase
{
	[Parameter] public Guid Id { get; init; }

	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IOptions<JsonSerializerOptions>? JsonSerializerOptions { get; init; }
	[Inject] private IOptions<ServerOptions>? ServerOptions { get; init; }
	[Inject] private ILogger<Details>? Logger { get; init; }

	private Character? character;
	private Character? original;
	private readonly List<Feature> selected = new();
	private bool saving;

	private bool showFeatureModal;
	private IEnumerable<Feature>? features;
	private int? featuresCount;
	private readonly List<Feature> selectedFeatures = new();

	protected override async Task OnParametersSetAsync()
	{
		if (this.Client is null || this.ServerOptions is null)
			return;

		this.character = await this.Client.GetFromJsonAsync<Character>(
			$"{this.ServerOptions.Value.Route}v1/Characters/{this.Id}?$expand=Features");

		if (this.character is not null)
			this.original = this.character.DeepClone();
	}

	private async Task Save()
	{
		this.saving = true;

		if (this.Client is null || this.ServerOptions is null || this.character is null)
			return;

		Dictionary<string, string> headers = new() { { "Content-Type", "application/json" } };

		IEnumerable<ODataRequest>? requests = this.original?.Features.Except(this.character.Features)
			.Select((Feature feature, int i) => new ODataRequest($"{i + 2}", "DELETE",
				new Uri($"{this.ServerOptions.Value.Route}v1/CharacterFeatures"), headers,
				new CharacterFeature
				{
					CharacterId = this.Id,
					FeatureId = feature.Id,
				}))
			.Prepend(new ODataRequest("1", "PUT",
				new Uri($"{this.ServerOptions.Value.Route}v1/Characters"), headers, this.character))
			.ToList();

		if (requests?.Any() == true)
			await this.Client.PostAsJsonAsync($"{this.ServerOptions.Value.Route}v1/$batch",
				new ODataBatchRequest(requests), this.JsonSerializerOptions?.Value);

		this.saving = false;
	}

	private void OnRowRemoving(CancellableRowChange<Feature> args)
	{
		if (this.character is null || this.character.Features?.Any() != true)
			return;

		foreach (Feature feature in this.selected)
		{
			this.character.Features.Remove(feature);
		}
	}

	private void OnAddFeatures()
	{
		if (this.character is null)
			return;

		if (this.character.Features is null)
			this.character.Features = new List<Feature>();

		foreach (Feature feature in this.selectedFeatures.ExceptBy(this.character.Features.Select(x => x.Id),
			(Feature feature) => feature.Id))
		{
			this.character.Features.Add(feature);
		}

		this.showFeatureModal = false;
	}

	private Task OnFeatureModalClosing(ModalClosingEventArgs args)
	{
		this.selectedFeatures.Clear();
		return Task.CompletedTask;
	}

	private async Task OnReadFeatures(DataGridReadDataEventArgs<Feature> args)
	{
		if (this.ReadDataCommand is null)
			return;

		ODataCollectionResponse<Feature>? response = await this.ReadDataCommand.ExecuteAsync(args, "v1/Features",
			filters: new[] { $"Characters/any(c:c/Id ne {this.Id})" });

		if (response is null)
			return;

		this.features = this.character?.Features?.Any() == true
			? response.Value.Except(this.character.Features).ToList()
			: response.Value.ToList();
		this.featuresCount = response.Count;

		StateHasChanged();
	}
}

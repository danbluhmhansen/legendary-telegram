namespace BlazorApp1.Client.Pages.Characters;

using System.Text.Json;
using System.Threading.Tasks;

using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Data;
using BlazorApp1.Client.Extensions;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.OData.Client;

public partial class Details : ComponentBase
{
	[Parameter] public Guid Id { get; init; }

	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private ComputeCharacterCommand? ComputeCharacterCommand { get; init; }
	[Inject] private ODataServiceContext? ServiceContext { get; init; }
	[Inject] private ILogger<Details>? Logger { get; init; }

	private Character? character;
	private ICollection<JsonProperty> jsonProperties = new List<JsonProperty>();

	private IEnumerable<Feature>? features;
	private int? featuresCount;
	private readonly List<Feature> selectedFeatures = new();

	protected override async Task OnParametersSetAsync()
	{
		if (this.ServiceContext is null)
			return;

		IQueryable<Character> query = this.ServiceContext.Characters
			.Expand($"{nameof(Character.Features)}($expand={nameof(Feature.Effects)})")
			.Expand(nameof(Character.Effects))
			.Where((Character character) => character.Id == this.Id);

		if (await query.ExecuteAsync() is not QueryOperationResponse<Character> response)
			return;

		this.character = response.First();

		if (this.ComputeCharacterCommand is not null && this.character is not null)
		{
			this.jsonProperties = this.ComputeCharacterCommand.Execute(this.character)
				.EnumerateObject()
					.Where((JsonProperty property) =>
						property.Value.ValueKind is JsonValueKind.String or JsonValueKind.Number)
					.ToList();
		}
	}

	private async Task OnReadFeatures(DataGridReadDataEventArgs<Feature> args)
	{
		if (this.ReadDataCommand is null)
			return;

		IQueryable<Feature> query = this.ReadDataCommand.Execute(args, "Features")
			.Where((Feature feature) => !feature.Characters.Any((Character character) => this.Id == character.Id));

		switch (args.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query = query.Skip((args.Page - 1) * args.PageSize).Take(args.PageSize);
				break;
			case DataGridReadDataMode.Virtualize:
				query = query.Skip(args.VirtualizeOffset).Take(args.VirtualizeCount);
				break;
		}

		QueryOperationResponse<Feature> response = await query.ExecuteAsync(args.CancellationToken);

		this.features = this.character?.Features?.Any() == true
			? response.Except(this.character.Features).ToList()
			: response.ToList();
		this.featuresCount = (int)response.Count;

		StateHasChanged();
	}

	private CoreEffect NewEffectCreator() => new() { CharacterId = this.Id, };

	private async Task OnEffectInserted(SavedRowItem<CoreEffect, Dictionary<string, object>> args)
	{
		if (this.ServiceContext is null)
			return;

		this.ServiceContext.AddObject("CoreEffects", args.Item);
		await this.ServiceContext.SaveChangesAsync();
	}

	private async Task OnEffectUpdated(SavedRowItem<CoreEffect, Dictionary<string, object>> args)
	{
		if (this.ServiceContext is null)
			return;

		this.ServiceContext.AttachTo("CoreEffects", args.Item);
		this.ServiceContext.UpdateObject(args.Item);
		await this.ServiceContext.SaveChangesAsync();
	}

	private async Task OnEffectRemoving(CancellableRowChange<CoreEffect> args)
	{
		if (this.ServiceContext is null)
			return;

		this.ServiceContext.AttachTo("CoreEffects", args.Item);
		this.ServiceContext.DeleteObject(args.Item);
		await this.ServiceContext.SaveChangesAsync();
	}

	private async Task OnFeatureInserting()
	{
		if (this.ServiceContext is null || this.selectedFeatures?.Any() != true)
			return;

		foreach (Feature feature in this.selectedFeatures)
		{
			this.ServiceContext.AddObject("CharacterFeatures",
				new CharacterFeature
				{
					CharacterId = this.Id,
					FeatureId = feature.Id,
				});
		}

		await this.ServiceContext.SaveChangesAsync();
	}

	private async Task OnFeatureRemoving()
	{
		if (this.ServiceContext is null || this.selectedFeatures?.Any() != true)
			return;

		foreach (CharacterFeature characterFeature in this.selectedFeatures.Select((Feature feature) =>
			new CharacterFeature
			{
				CharacterId = this.Id,
				FeatureId = feature.Id,
			}))
		{
			this.ServiceContext.AttachTo("CharacterFeatures", characterFeature);
			this.ServiceContext.DeleteObject(characterFeature);
		}

		await this.ServiceContext.SaveChangesAsync();
	}
}

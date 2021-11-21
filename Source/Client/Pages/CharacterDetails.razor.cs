namespace BlazorApp1.Client.Pages;

using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Models.v1;
using Blazorise;
using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

public partial class CharacterDetails : ComponentBase
{
	[Parameter] public Guid Id { get; init; }

	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IConfiguration? Configuration { get; init; }
	[Inject] private ILogger<CharacterDetails>? Logger { get; init; }

	private Character? character;
	private readonly List<Feature> selected = new();
	private bool saving;

	private bool showFeatureModal;
	private IEnumerable<Feature>? features;
	private int? featuresCount;
	private readonly List<Feature> selectedFeatures = new();

	protected override async Task OnParametersSetAsync()
	{
		if (this.Client is null)
			return;

		this.character = await this.Client.GetFromJsonAsync<Character>(
			$"{this.Configuration.GetValue<string>("ServerUrl")}v1/Characters/{this.Id}");
	}

	private async Task Save()
	{
		this.saving = true;

		if (this.Client is null || this.character is null)
			return;

		JsonSerializerOptions jsonSerializerOptions = new()
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};

		HttpResponseMessage response = await this.Client.PutAsJsonAsync(
			$"{this.Configuration.GetValue<string>("ServerUrl")}v1/Characters",
			this.character,
			jsonSerializerOptions);

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

		foreach (Feature feature in this.selectedFeatures.Except(this.character.Features))
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
		if (this.Client is null)
			return;

		IEnumerable<string> filters = args.Columns
			.Where(column => column.SearchValue is not null)
			.Select(column => column.ColumnType switch
			{
				DataGridColumnType.Text => $"contains({column.Field},'{column.SearchValue}')",
				_ => "",
			});

		IEnumerable<string> orderBy = args.Columns
			.Where(column => column.SortDirection is not SortDirection.None)
			.OrderBy(column => column.SortIndex)
			.Select(column =>
				column.SortDirection is SortDirection.Ascending ? column.Field : $"{column.Field} desc");

		UriBuilder uriBuilder = new($"{this.Configuration.GetValue<string>("ServerUrl")}v1/Features");

		NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

		if (filters.Any())
			query["$filter"] = string.Join(", ", filters);
		if (orderBy.Any())
			query["$orderby"] = string.Join(", ", orderBy);

		switch (args.ReadDataMode)
		{
			case DataGridReadDataMode.Paging:
				query["$skip"] = ((args.Page - 1) * args.PageSize).ToString();
				query["$top"] = args.PageSize.ToString();
				break;
			case DataGridReadDataMode.Virtualize:
				query["$skip"] = args.VirtualizeOffset.ToString();
				query["$top"] = args.VirtualizeCount.ToString();
				break;
		}

		query["$count"] = "true";

		uriBuilder.Query = query.ToString();

		ODataResponse<Feature>? response = await this.Client.GetFromJsonAsync<ODataResponse<Feature>>(
			uriBuilder.Uri, args.CancellationToken);

		if (response is null)
			return;

		this.features = this.character?.Features?.Any() == true
			? response.Value.Except(this.character.Features).ToList()
			: response.Value.ToList();
		this.featuresCount = response.Count;

		StateHasChanged();
	}
}

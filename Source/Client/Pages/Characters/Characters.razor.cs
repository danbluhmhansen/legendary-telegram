namespace BlazorApp1.Client.Pages.Characters;

using BlazorApp1.Client.Commands;
using BlazorApp1.Client.Models;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

public partial class Characters : ComponentBase
{
	[Inject] private ReadDataCommand? ReadDataCommand { get; init; }
	[Inject] private NavigationManager? Navigation { get; init; }

	private ICollection<Character> data = new List<Character>();
	private int? count;

	private async Task OnReadData(DataGridReadDataEventArgs<Character> args)
	{
		if (this.ReadDataCommand is null)
			return;

		ODataCollectionResponse<Character>? response = await this.ReadDataCommand.ExecuteAsync(args, "v1/Characters");

		if (response is null)
			return;

		this.data = response.Value.ToList();
		this.count = response.Count;

		StateHasChanged();
	}

	private void OnSelectRow(Character args) => this.Navigation?.NavigateTo($"/Characters/{args.Id}");
}
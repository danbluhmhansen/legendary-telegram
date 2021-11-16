namespace BlazorApp1.Client.Pages;

using System.Net.Http.Json;
using System.Threading.Tasks;

using BlazorApp1.Shared.Models.v1;

using Microsoft.AspNetCore.Components;

public partial class CharacterDetails : ComponentBase
{
	[Parameter] public Guid Id { get; init; }

	[Inject] private HttpClient? Client { get; init; }
	[Inject] private IConfiguration? Configuration { get; init; }

	private Character? character;

	protected override async Task OnParametersSetAsync()
	{
		if (this.Client is null)
			return;

		this.character = await this.Client.GetFromJsonAsync<Character>(
			$"{this.Configuration.GetValue<string>("ServerUrl")}v1/characters/{Id}");
	}
}

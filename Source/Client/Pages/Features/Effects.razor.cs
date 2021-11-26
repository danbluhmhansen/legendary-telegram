namespace BlazorApp1.Client.Pages.Features;

using System.Text.Json;
using BlazorApp1.Shared.Models.v1;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

public partial class Effects : ComponentBase
{
	[Parameter] public Feature? Feature { get; init; }
}

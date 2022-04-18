namespace LegendaryTelegram.Server.Pages.Authorization;

using Microsoft.AspNetCore.Components;

public partial class Authorize : ComponentBase
{
    [Parameter] public string? ApplicationName { get; set; }
    [Parameter] public string? Scope { get; set; }
}

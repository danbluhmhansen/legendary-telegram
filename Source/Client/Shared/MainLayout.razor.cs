using LegendaryTelegram.Client.Components;
using LegendaryTelegram.Client.Models;

using Microsoft.AspNetCore.Components;

namespace LegendaryTelegram.Client.Shared;

public partial class MainLayout : LayoutComponentBase, IErrorComponent
{
    private Error? error;

    public void ShowError(Error error)
    {
        this.error = error;
        StateHasChanged();
    }

    private void HideError() => error = null;
}


namespace BlazorApp1.Client.Pages.Features;

using BlazorApp1.Client.Data;
using BlazorApp1.Shared.Models.v1;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

public partial class Effects : ComponentBase
{
    [Parameter] public Feature? Feature { get; init; }

    [Inject] private ODataServiceContext? ServiceContext { get; init; }

    private async Task OnRowRemoving(CancellableRowChange<Effect> args)
    {
        if (this.ServiceContext is null || args.Item is null)
            return;

        this.ServiceContext.AttachTo("Features", args.Item);
        this.ServiceContext.DeleteObject(args.Item);
        await this.ServiceContext.SaveChangesAsync();
    }
}

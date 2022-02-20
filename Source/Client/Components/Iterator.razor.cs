namespace BlazorApp1.Client.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

public partial class Iterator<T> : ComponentBase
{
    [Parameter] public IEnumerable<T>? Items { get; init; }

    [Parameter] public RenderFragment<IteratorContext<T>>? ChildContent { get; init; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (this.Items?.Any() != true || this.ChildContent is null)
            return;

        int index = 0;
        foreach (T item in this.Items)
        {
            builder.AddContent(0, this.ChildContent(new IteratorContext<T>(index++, item)));
        }
    }
}

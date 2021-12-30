namespace BlazorApp1.Client.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

public partial class Condition : ComponentBase
{
	[Parameter] public bool Evaluation { get; init; }
	[Parameter] public RenderFragment? True { get; init; }
	[Parameter] public RenderFragment? False { get; init; }

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		if (this.Evaluation)
			builder.AddContent(0, this.True);
		else
			builder.AddContent(0, this.False);
	}
}

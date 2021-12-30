namespace BlazorApp1.Client.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

public partial class Condition : ComponentBase
{
	[Parameter] public bool Evaluation { get; init; }
	[Parameter] public RenderFragment? Match { get; init; }
	[Parameter] public RenderFragment? NotMatch { get; init; }

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		if (this.Evaluation)
			builder.AddContent(0, this.Match);
		else
			builder.AddContent(0, this.NotMatch);
	}
}

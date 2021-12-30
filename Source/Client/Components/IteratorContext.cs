namespace BlazorApp1.Client.Components;

using Microsoft.AspNetCore.Components;

public class IteratorContext<T> : ComponentBase
{
	public IteratorContext(int index, T? item)
	{
		this.Index = index;
		this.Item = item;
	}

	public int Index { get; init; }
	public T? Item { get; init; }
}

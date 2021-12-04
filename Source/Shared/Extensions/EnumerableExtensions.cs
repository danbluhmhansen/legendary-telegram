namespace BlazorApp1.Shared.Extensions;

public static class EnumerableExtensions
{
	public static IEnumerable<T> Yield<T>(this T t)
	{
		yield return t;
	}

	public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source) =>
		source.SelectMany((IEnumerable<T> source) => source);
}

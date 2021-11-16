namespace BlazorApp1.Server.Helpers;

public static class AsyncEnumerableExtensions
{
	public static Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
	{
		return source is null ? throw new ArgumentNullException(nameof(source)) : ExecuteAsync();

		async Task<List<T>> ExecuteAsync()
		{
			var list = new List<T>();

			await foreach (T? element in source)
				list.Add(element);

			return list;
		}
	}
}

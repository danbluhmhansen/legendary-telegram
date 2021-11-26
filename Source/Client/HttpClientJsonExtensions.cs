namespace BlazorApp1.Client;

using System.Net.Http.Json;
using System.Text.Json;

public static class HttpClientJsonExtensions
{
	public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(
		this HttpClient client, string? requestUri, T value, JsonSerializerOptions? options = default,
		CancellationToken cancellationToken = default)
	{
		HttpRequestMessage request = new(HttpMethod.Delete, requestUri)
		{
			Content = JsonContent.Create(value, options: options)
		};
		return client.SendAsync(request, cancellationToken);
	}

	public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(
		this HttpClient client, Uri? requestUri, T value, JsonSerializerOptions? options = default,
		CancellationToken cancellationToken = default)
	{
		HttpRequestMessage request = new(HttpMethod.Delete, requestUri)
		{
			Content = JsonContent.Create(value, options: options)
		};
		return client.SendAsync(request, cancellationToken);
	}

	public static Task<HttpResponseMessage> DeleteAsJsonAsync(
		this HttpClient client, string? requestUri, object value, JsonSerializerOptions? options = default,
		CancellationToken cancellationToken = default) =>
		client.DeleteAsJsonAsync<object>(requestUri, value, options, cancellationToken);

	public static Task<HttpResponseMessage> DeleteAsJsonAsync(
		this HttpClient client, Uri? requestUri, object value, JsonSerializerOptions? options = default,
		CancellationToken cancellationToken = default) =>
		client.DeleteAsJsonAsync<object>(requestUri, value, options, cancellationToken);
}

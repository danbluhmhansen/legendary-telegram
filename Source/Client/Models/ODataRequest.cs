namespace BlazorApp1.Client.Models;

public record ODataRequest(
	string Id,
	string Method,
	Uri Url,
	IDictionary<string, string>? Headers = default,
	object? Body = default);

public record ODataRequest<T>(
	string Id,
	string Method,
	Uri Url,
	IDictionary<string, string>? Headers = default,
	T? Body = default);

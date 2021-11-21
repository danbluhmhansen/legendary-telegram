namespace BlazorApp1.Client.Models;

public record ODataRequest(
	string Id,
	string Method,
	Uri Url,
	IDictionary<string, IEnumerable<string>>? Headers,
	object? Body)
{
	public ODataRequest(string id, string method, Uri url) : this(id, method, url, default, default) { }
}

public record ODataRequest<T>(
	string Id,
	string Method,
	Uri Url,
	IDictionary<string, IEnumerable<string>>? Headers,
	T? Body)
{
	public ODataRequest(string id, string method, Uri url) : this(id, method, url, default, default) { }
}

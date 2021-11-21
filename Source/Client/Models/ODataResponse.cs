namespace BlazorApp1.Client.Models;

using System.Net;

public record ODataResponse(
	string Id,
	HttpStatusCode Status,
	IDictionary<string, IEnumerable<string>>? Headers,
	object Body);

public record ODataResponse<T>(
	string Id,
	HttpStatusCode Status,
	IDictionary<string, IEnumerable<string>>? Headers,
	T Body);

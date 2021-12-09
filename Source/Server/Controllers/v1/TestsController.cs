namespace BlazorApp1.Server.Controllers;

using System.Text.Json.Nodes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

[ApiVersion("1.0")]
public class TestsController : ODataController
{
	[HttpGet, EnableQuery]
	public IEnumerable<Test> Get()
	{
		return new[]
		{
			new Test(Guid.NewGuid(), "Test", new JsonObject { { "+", new JsonArray(new JsonObject { { "var", "Strength" } }, 2) } }),
		};
	}
}

public record Test(Guid Id, string Name, JsonNode Node);

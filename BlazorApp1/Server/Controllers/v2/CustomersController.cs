namespace BlazorApp1.Server.Controllers.v2;

using BlazorApp1.Server.Models.v2;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[ApiVersion("2.0")]
public class CustomersController : ControllerBase
{
	private readonly Customer[] customers = new Customer[]
	{
		new Customer
		{
			Id = 11,
			ApiVersion = "v2.0",
			FirstName = "YXS",
			LastName = "WU",
			Email = "yxswu@abc.com"
		},
		new Customer
		{
			Id = 12,
			ApiVersion = "v2.0",
			FirstName = "KIO",
			LastName = "XU",
			Email = "kioxu@efg.com"
		}
	};

	[EnableQuery]
	public IActionResult Get() => Ok(this.customers);

	[EnableQuery]
	public IActionResult Get(int key)
	{
		Customer? customer = this.customers.FirstOrDefault(c => c.Id == key);
		return customer == null ? NotFound($"Cannot find customer with Id={key}.") : Ok(customer);
	}

}

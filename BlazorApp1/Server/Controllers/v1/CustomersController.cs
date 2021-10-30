namespace BlazorApp1.Server.Controllers.v1;

using BlazorApp1.Server.Models.v1;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[ApiVersion("1.0")]
[Route("v1/[controller]")]
public class CustomersController : ControllerBase
{
	private readonly Customer[] customers = new Customer[]
	{
		new Customer
		{
			Id = 1,
			ApiVersion = "v1.0",
			Name = "Sam",
			PhoneNumber = "111-222-3333"
		},
		new Customer
		{
			Id = 2,
			ApiVersion = "v1.0",
			Name = "Peter",
			PhoneNumber = "456-ABC-8888"
		}
	};

	[HttpGet]
	[EnableQuery]
	public IActionResult Get() => Ok(this.customers);

	[HttpGet("{key}")]
	[EnableQuery]
	public IActionResult Get(int key)
	{
		Customer? customer = this.customers.FirstOrDefault(c => c.Id == key);
		return customer == null ? NotFound($"Cannot find customer with Id={key}.") : Ok(customer);
	}
}

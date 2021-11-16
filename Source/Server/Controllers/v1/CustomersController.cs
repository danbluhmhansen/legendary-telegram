namespace BlazorApp1.Server.Controllers.v1;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BlazorApp1.Server.Data;
using BlazorApp1.Server.ViewModels.v1;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

[ApiVersion("1.0")]
[Route("v1/[controller]")]
public class CustomersController : ODataController
{
	private readonly ApplicationDbContext dbContext;
	private readonly IMapper mapper;

	public CustomersController(ApplicationDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	[HttpGet, EnableQuery]
	public IQueryable<Customer> Get() => this.mapper.ProjectTo<Customer>(this.dbContext.Customers);

	[HttpGet("{key}"), EnableQuery]
	public async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
	{
		Models.Customer? entity = await this.dbContext.Customers.FindAsync(key).ConfigureAwait(false);
		return entity is not null ? Ok(this.mapper.Map<Customer>(entity)) : NotFound(key);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Post([FromBody, Required] Customer input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		EntityEntry<Models.Customer> entityEntry = this.dbContext.Customers
			.Add(this.mapper.Map<Models.Customer>(input));
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<Customer>(entityEntry.Entity));
	}

	[HttpPut]
	public async ValueTask<IActionResult> Put(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Delta<Customer> input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Models.Customer? entity = await this.dbContext.Customers.FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		Customer model = this.mapper.Map<Customer>(entity);
		input.Put(model);
		this.mapper.Map(model, entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<Customer>(entity));
	}

	[HttpPatch]
	public async ValueTask<IActionResult> Patch(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Delta<Customer> input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Models.Customer? entity = await this.dbContext.Customers.FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		Customer model = this.mapper.Map<Customer>(entity);
		input.Patch(model);
		this.mapper.Map(model, entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<Customer>(entity));
	}

	[HttpDelete]
	public async ValueTask<IActionResult> Delete([FromODataUri, Required] Guid key)
	{
		Models.Customer? entity = await this.dbContext.Customers.FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		this.dbContext.Customers.Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

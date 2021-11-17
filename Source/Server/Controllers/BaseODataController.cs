namespace BlazorApp1.Server.Controllers;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BlazorApp1.Server.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public abstract class BaseODataController<TModel, TEntity> : ODataController
	where TModel : class
	where TEntity : class
{
	protected readonly ApplicationDbContext dbContext;
	protected readonly IMapper mapper;

	protected BaseODataController(ApplicationDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	[HttpGet, EnableQuery]
	public IQueryable<TModel> Get() => this.mapper.ProjectTo<TModel>(this.dbContext.Set<TEntity>());

	[HttpGet("{key}"), EnableQuery]
	public async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
	{
		TEntity? entity = await this.dbContext.Set<TEntity>().FindAsync(key).ConfigureAwait(false);
		return entity is not null ? Ok(this.mapper.Map<TModel>(entity)) : NotFound(key);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Post([FromBody, Required] TModel input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		EntityEntry<TEntity> entityEntry = this.dbContext.Set<TEntity>()
			.Add(this.mapper.Map<TEntity>(input));
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<TModel>(entityEntry.Entity));
	}

	[HttpPut]
	public async ValueTask<IActionResult> Put(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Delta<TModel> input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		TEntity? entity = await this.dbContext.Set<TEntity>().FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		TModel model = this.mapper.Map<TModel>(entity);
		input.Put(model);
		this.mapper.Map(model, entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<TModel>(entity));
	}

	[HttpPatch]
	public async ValueTask<IActionResult> Patch(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Delta<TModel> input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		TEntity? entity = await this.dbContext.Set<TEntity>().FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		TModel model = this.mapper.Map<TModel>(entity);
		input.Patch(model);
		this.mapper.Map(model, entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<TModel>(entity));
	}

	[HttpDelete]
	public async ValueTask<IActionResult> Delete([FromODataUri, Required] Guid key)
	{
		TEntity? entity = await this.dbContext.Set<TEntity>().FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		this.dbContext.Set<TEntity>().Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

namespace BlazorApp1.Server.Controllers;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BlazorApp1.Server.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

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
	public virtual IQueryable<TModel> Get() => this.mapper.ProjectTo<TModel>(this.dbContext.Set<TEntity>());

	[HttpGet, EnableQuery]
	public virtual async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
	{
		TModel? model = await this.mapper.ProjectTo<TModel>(
			this.dbContext.Set<TEntity>().Where((TEntity entity) => key == EF.Property<Guid>(entity, "Id")))
			.FirstOrDefaultAsync().ConfigureAwait(false);
		return model is not null ? Ok(model) : NotFound(key);
	}

	[HttpPost]
	public virtual async ValueTask<IActionResult> Post([FromBody, Required] TModel input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		TEntity entity = this.mapper.Map<TEntity>(input);
		this.dbContext.Set<TEntity>().Add(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<TModel>(entity));
	}

	[HttpPut]
	public virtual async ValueTask<IActionResult> Put([FromBody, Required] TModel input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		TEntity entity = this.mapper.Map<TEntity>(input);
		this.dbContext.Set<TEntity>().Update(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<TModel>(entity));
	}

	[HttpDelete]
	public virtual async ValueTask<IActionResult> Delete([FromBody, Required] TModel input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		TEntity entity = this.mapper.Map<TEntity>(input);
		this.dbContext.Set<TEntity>().Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

namespace BlazorApp1.Server.Controllers.v1;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BlazorApp1.Server.Data;
using BlazorApp1.Shared.Models.v1;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

[ApiVersion("1.0")]
public class FeaturesController : ODataController
{
	private readonly ApplicationDbContext dbContext;
	private readonly IMapper mapper;

	public FeaturesController(ApplicationDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	[HttpGet, EnableQuery]
	public virtual IQueryable<Feature> Get() => this.mapper.ProjectTo<Feature>(this.dbContext.Features);

	[HttpGet, EnableQuery]
	public virtual async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
	{
		Entities.Feature? entity = await this.dbContext.Features.FindAsync(key);
		Feature? model = this.mapper.Map<Feature>(entity);
		return model is not null ? Ok(model) : NotFound(key);
	}

	[HttpPost]
	public virtual async ValueTask<IActionResult> Post([FromBody, Required] Feature input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Entities.Feature entity = this.mapper.Map<Entities.Feature>(input);
		this.dbContext.Add(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<Feature>(entity));
	}

	[HttpPut]
	public virtual async ValueTask<IActionResult> Put(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Feature input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		input.Id = key;

		Entities.Feature entity = this.mapper.Map<Entities.Feature>(input);
		this.dbContext.Update(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<Feature>(entity));
	}

	[HttpDelete]
	public virtual async ValueTask<IActionResult> Delete([FromODataUri, Required] Guid key)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Feature input = new() { Id = key, };

		Entities.Feature entity = this.mapper.Map<Entities.Feature>(input);
		this.dbContext.Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

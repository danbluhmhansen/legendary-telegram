namespace BlazorApp1.Server.Controllers.v1;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BlazorApp1.Server.Data;
using BlazorApp1.Shared.Models.v1;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

[ApiVersion("1.0")]
public class CharactersController : ODataController
{
	private readonly ApplicationDbContext dbContext;
	private readonly IMapper mapper;

	public CharactersController(ApplicationDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	[HttpGet, EnableQuery]
	public virtual IQueryable<Character> Get() => this.mapper.ProjectTo<Character>(this.dbContext.Characters);

	[HttpGet, EnableQuery]
	public virtual async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
	{
		Character? model = await this.mapper.ProjectTo<Character>(
			this.dbContext.Characters.Where((Entities.Character entity) => entity.Id == key))
			.FirstOrDefaultAsync().ConfigureAwait(false);
		return model is not null ? Ok(model) : NotFound(key);
	}

	[HttpPost]
	public virtual async ValueTask<IActionResult> Post([FromBody, Required] Character input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Entities.Character entity = this.mapper.Map<Entities.Character>(input);
		this.dbContext.Add(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<Character>(entity));
	}

	[HttpPut]
	public virtual async ValueTask<IActionResult> Put(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Character input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		input.Id = key;

		Entities.Character entity = this.mapper.Map<Entities.Character>(input);
		this.dbContext.Update(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<Character>(entity));
	}

	[HttpDelete]
	public virtual async ValueTask<IActionResult> Delete([FromODataUri, Required] Guid key)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Character input = new() { Id = key, };

		Entities.Character entity = this.mapper.Map<Entities.Character>(input);
		this.dbContext.Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

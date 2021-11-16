namespace BlazorApp1.Server.Controllers.v1;

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using BlazorApp1.Server.Data;
using BlazorApp1.Shared.Models.v1;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

[ApiVersion("1.0")]
[Route("v1/Characters")]
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
	public IQueryable<Character> Get() => this.mapper.ProjectTo<Character>(this.dbContext.Characters);

	[HttpGet("{key}"), EnableQuery]
	public async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
	{
		Entities.Character? entity = await this.dbContext.Characters.FindAsync(key).ConfigureAwait(false);
		return entity is not null ? Ok(this.mapper.Map<Character>(entity)) : NotFound(key);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Post([FromBody, Required] Character input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		EntityEntry<Entities.Character> entityEntry = this.dbContext.Characters
			.Add(this.mapper.Map<Entities.Character>(input));
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<Character>(entityEntry.Entity));
	}

	[HttpPut]
	public async ValueTask<IActionResult> Put(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Delta<Character> input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Entities.Character? entity = await this.dbContext.Characters.FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		Character model = this.mapper.Map<Character>(entity);
		input.Put(model);
		this.mapper.Map(model, entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<Character>(entity));
	}

	[HttpPatch]
	public async ValueTask<IActionResult> Patch(
		[FromODataUri, Required] Guid key,
		[FromBody, Required] Delta<Character> input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Entities.Character? entity = await this.dbContext.Characters.FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		Character model = this.mapper.Map<Character>(entity);
		input.Patch(model);
		this.mapper.Map(model, entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Updated(this.mapper.Map<Character>(entity));
	}

	[HttpDelete]
	public async ValueTask<IActionResult> Delete([FromODataUri, Required] Guid key)
	{
		Entities.Character? entity = await this.dbContext.Characters.FindAsync(key).ConfigureAwait(false);

		if (entity is null)
			return NotFound(key);

		this.dbContext.Characters.Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

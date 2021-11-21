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
public class CharacterFeaturesController : ODataController
{
	private readonly ApplicationDbContext dbContext;
	private readonly IMapper mapper;

	public CharacterFeaturesController(ApplicationDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	[HttpGet, EnableQuery]
	public IQueryable<CharacterFeature> Get() =>
		this.mapper.ProjectTo<CharacterFeature>(this.dbContext.Set<Entities.CharacterFeature>());

	[HttpPost]
	public async ValueTask<IActionResult> Post([FromBody, Required] CharacterFeature input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Entities.CharacterFeature entity = this.mapper.Map<Entities.CharacterFeature>(input);
		this.dbContext.Set<Entities.CharacterFeature>().Update(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return Created(this.mapper.Map<CharacterFeature>(entity));
	}

	[HttpDelete]
	public async ValueTask<IActionResult> Delete([FromBody, Required] CharacterFeature input)
	{
		if (!this.ModelState.IsValid)
			return BadRequest(this.ModelState);

		Entities.CharacterFeature entity = this.mapper.Map<Entities.CharacterFeature>(input);
		this.dbContext.Set<Entities.CharacterFeature>().Remove(entity);
		await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

		return NoContent();
	}
}

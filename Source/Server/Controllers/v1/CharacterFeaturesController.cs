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
    public virtual IQueryable<CharacterFeature> Get() =>
        this.mapper.ProjectTo<CharacterFeature>(this.dbContext.CharacterFeatures);

    [HttpGet, EnableQuery]
    public virtual async ValueTask<IActionResult> Get(
        [FromODataUri, Required] Guid keyCharacterId,
        [FromODataUri, Required] Guid keyFeatureId)
    {
        CharacterFeature? model = await this.mapper.ProjectTo<CharacterFeature>(
            this.dbContext.CharacterFeatures.Where((Entities.CharacterFeature entity) =>
                entity.CharacterId == keyCharacterId
                && entity.FeatureId == keyFeatureId))
            .FirstOrDefaultAsync().ConfigureAwait(false);
        return model is not null ? Ok(model) : NotFound(new { keyCharacterId, keyFeatureId });
    }

    [HttpPost]
    public virtual async ValueTask<IActionResult> Post([FromBody, Required] CharacterFeature input)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(this.ModelState);

        Entities.CharacterFeature entity = this.mapper.Map<Entities.CharacterFeature>(input);
        this.dbContext.Add(entity);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

        return Created(this.mapper.Map<CharacterFeature>(entity));
    }

    [HttpDelete]
    public virtual async ValueTask<IActionResult> Delete(
        [FromODataUri, Required] Guid keyCharacterId,
        [FromODataUri, Required] Guid keyFeatureId)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(this.ModelState);

        CharacterFeature input = new()
        {
            CharacterId = keyCharacterId,
            FeatureId = keyFeatureId,
        };

        Entities.CharacterFeature entity = this.mapper.Map<Entities.CharacterFeature>(input);
        this.dbContext.Remove(entity);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

        return NoContent();
    }
}

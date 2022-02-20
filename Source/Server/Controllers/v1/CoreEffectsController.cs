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
public class CoreEffectsController : ODataController
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper mapper;

    public CoreEffectsController(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpGet, EnableQuery]
    public virtual IQueryable<CoreEffect> Get() => this.mapper.ProjectTo<CoreEffect>(this.dbContext.CoreEffects);

    [HttpGet, EnableQuery]
    public virtual async ValueTask<IActionResult> Get([FromODataUri, Required] Guid key)
    {
        CoreEffect? model = await this.mapper.ProjectTo<CoreEffect>(
            this.dbContext.CoreEffects.Where((Entities.CoreEffect entity) => entity.Id == key))
            .FirstOrDefaultAsync().ConfigureAwait(false);
        return model is not null ? Ok(model) : NotFound(key);
    }

    [HttpPost]
    public virtual async ValueTask<IActionResult> Post([FromBody, Required] CoreEffect input)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(this.ModelState);

        Entities.CoreEffect entity = this.mapper.Map<Entities.CoreEffect>(input);
        this.dbContext.Add(entity);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

        return Created(this.mapper.Map<CoreEffect>(entity));
    }

    [HttpPut]
    public virtual async ValueTask<IActionResult> Put(
        [FromODataUri, Required] Guid key,
        [FromBody, Required] CoreEffect input)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(this.ModelState);

        input.Id = key;

        Entities.CoreEffect entity = this.mapper.Map<Entities.CoreEffect>(input);
        this.dbContext.Update(entity);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

        return Updated(this.mapper.Map<CoreEffect>(entity));
    }

    [HttpDelete]
    public virtual async ValueTask<IActionResult> Delete([FromODataUri, Required] Guid key)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(this.ModelState);

        CoreEffect input = new() { Id = key, };

        Entities.CoreEffect entity = this.mapper.Map<Entities.CoreEffect>(input);
        this.dbContext.Remove(entity);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

        return NoContent();
    }
}

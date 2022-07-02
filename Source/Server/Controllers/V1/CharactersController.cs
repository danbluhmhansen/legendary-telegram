using Asp.Versioning;

using LegendaryTelegram.Server.Data;
using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace LegendaryTelegram.Server.Controllers.V1;

[ApiVersion(1.0)]
public class CharactersController : ODataController
{
  public CharactersController(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext;
  }

  private readonly ApplicationDbContext dbContext;

    [HttpGet]
    [EnableQuery]
    public IQueryable<Character> Get() => this.dbContext.Characters;

    [HttpGet]
    public async Task<IActionResult> Get(Guid id, ODataQueryOptions<Character> options)
    {
      return Ok(await ((IQueryable<Character>)options.ApplyTo(this.dbContext.Characters)).FirstOrDefaultAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Character character)
    {
      this.dbContext.Add(character);
      await this.dbContext.SaveChangesAsync();
      return CreatedAtAction(nameof(Get), new { id = character.Id });
    }
}


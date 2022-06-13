using LegendaryTelegram.Server.Data;
using LegendaryTelegram.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegendaryTelegram.Server.Controllers.V1;

[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
  public CharactersController(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext;
  }

  private readonly ApplicationDbContext dbContext;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await this.dbContext.Characters.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id) => Ok(await this.dbContext.Characters.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Character character)
    {
      this.dbContext.Add(character);
      await this.dbContext.SaveChangesAsync();
      return CreatedAtAction(nameof(Get), new { id = character.Id });
    }
}


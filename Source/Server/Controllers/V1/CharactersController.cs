using Asp.Versioning;
using Asp.Versioning.OData;
using LegendaryTelegram.Server.Data;
using LegendaryTelegram.Server.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace LegendaryTelegram.Server.Controllers.V1;

/// <summary><see cref="Character"/>s service.</summary>
[ApiVersion(1.0)]
public class CharactersController : ODataController
{
  /// <summary>Initialises a new instance of <see cref="CharactersController"/>.</summary>
  public CharactersController(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext;
  }

  private readonly ApplicationDbContext dbContext;

  /**
   * <summary>Query <see cref="Character"/>s.</summary>
   * <param name="options">OData query options.</param>
   * <returns><see cref="Character"/>s matching the supplied query.</returns>
   * <response code="200">The successfully queried <see cref="Character"/>s.</response>
   */
  [HttpGet]
  [ProducesResponseType(typeof(ODataValue<IQueryable<Character>>), Status200OK, "application/json")]
  public IActionResult Get(ODataQueryOptions<Character> options) => Ok(options.ApplyTo(this.dbContext.Characters));

  /**
   * <summary>Find a <see cref="Character"/>.</summary>
   * <param name="key">The <see cref="Character"/>s identifier.</param>
   * <param name="options">OData query options.</param>
   * <returns>A <see cref="Character"/> matching the supplied query.</returns>
   * <response code="200">The found <see cref="Character"/>.</response>
   * <response code="404">The <see cref="Character"/> were not found.</response>
   */
  [HttpGet]
  [ProducesResponseType(typeof(Character), Status200OK, "application/json")]
  [ProducesResponseType(Status404NotFound)]
  public async Task<IActionResult> Get(Guid key, ODataQueryOptions<Character> options)
  {
    Character? character = await ((IQueryable<Character>)options.ApplyTo(this.dbContext.Characters)).FirstOrDefaultAsync();
    return character is not null ? Ok(character) : NotFound(new { id = key });
  }

  /**
   * <summary>Add a <see cref="Character"/>.</summary>
   * <param name="character">The <see cref="Character"/> to add.</param>
   * <returns>The added <see cref="Character"/>.</returns>
   * <response code="201">The <see cref="Character"/> were successfully added.</response>
   * <response code="400">The <paramref name="character"/> were invalid.</response>
   */
  [HttpPost]
  [ProducesResponseType(Status201Created)]
  [ProducesResponseType(Status400BadRequest)]
  public async Task<IActionResult> Post([FromBody] Character character)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    this.dbContext.Add(character);
    await this.dbContext.SaveChangesAsync();
    return CreatedAtAction(nameof(Get), new { id = character.Id });
  }

  /**
   * <summary>Update a <see cref="Character"/>.</summary>
   * <param name="key">The <see cref="Character"/>s identifier.</param>
   * <param name="character">The <see cref="Character"/> to update.</param>
   * <response code="200">The <see cref="Character"/> were successfully updated.</response>
   * <response code="400">The <paramref name="character"/> were invalid.</response>
   * <response code="404">The <see cref="Character"/> were not found.</response>
   */
  [HttpPut]
  [ProducesResponseType(typeof(Character), Status200OK, "application/json")]
  [ProducesResponseType(Status400BadRequest)]
  [ProducesResponseType(Status404NotFound)]
  public async Task<IActionResult> Put(Guid key, [FromBody] Character character)
  {
    character.Id = key;

    if (!ModelState.IsValid) return BadRequest(ModelState);

    this.dbContext.Update(character);

    try { await this.dbContext.SaveChangesAsync(); }
    catch (DbUpdateConcurrencyException) { return NotFound(new { id = key}); }

    return Ok(character);
  }

  /**
   * <summary>Remove a <see cref="Character"/>.</summary>
   * <param name="key">The <see cref="Character"/>s identifier.</param>
   * <returns>No content.</returns>
   * <response code="204">No content.</response>
   */
  [HttpDelete]
  [ProducesResponseType(Status204NoContent)]
  public async Task<IActionResult> Delete(Guid key)
  {
    Character character = new() { Id = key, };
    this.dbContext.Remove(character);

    try { await this.dbContext.SaveChangesAsync(); }
    catch (DbUpdateConcurrencyException) { }

    return NoContent();
  }
}


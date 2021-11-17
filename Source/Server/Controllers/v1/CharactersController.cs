namespace BlazorApp1.Server.Controllers.v1;

using AutoMapper;

using BlazorApp1.Server.Data;
using BlazorApp1.Shared.Models.v1;

using Microsoft.AspNetCore.Mvc;

[ApiVersion("1.0")]
public class CharactersController : BaseODataController<Character, Entities.Character>
{
	public CharactersController(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
}

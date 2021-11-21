namespace BlazorApp1.Server.Controllers.v1;

using AutoMapper;

using BlazorApp1.Server.Data;
using BlazorApp1.Shared.Models.v1;

using Microsoft.AspNetCore.Mvc;

[ApiVersion("1.0")]
public class CharacterFeaturesController : BaseODataController<CharacterFeature, Entities.CharacterFeature>
{
	public CharacterFeaturesController(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
}

using Asp.Versioning;
using Asp.Versioning.OData;

using LegendaryTelegram.Server.Models;

using Microsoft.OData.ModelBuilder;

namespace LegendaryTelegram.Server.Configurations;

public class CharactersConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        var characters = builder.EntitySet<Character>("Characters").EntityType;
        characters.HasKey(character => character.Id);
    }
}


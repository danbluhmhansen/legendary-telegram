namespace BlazorApp1.OData.Model;

using System;
using System.Collections.Generic;

using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

public class ODataModelProvider : IODataModelProvider
{
    private readonly IDictionary<string, IEdmModel> models = new Dictionary<string, IEdmModel>();

    public IEdmModel GetEdmModel(string apiVersion)
    {
        if (this.models.TryGetValue(apiVersion, out IEdmModel model))
        {
            return model;
        }

        model = BuildEdmModel(apiVersion);
        this.models[apiVersion] = model;
        return model;
    }

    private static IEdmModel BuildEdmModel(string version)
    {
        return version switch
        {
            "1" => BuildV1Model(),
            _ => throw new NotSupportedException($"The input version '{version}' is not supported!"),
        };
    }

    private static IEdmModel BuildV1Model()
    {
        ODataConventionModelBuilder oDataBuilder = new();
        oDataBuilder.EntitySet<Shared.Models.v1.Character>("Characters");
        oDataBuilder.EntitySet<Shared.Models.v1.Feature>("Features");
        oDataBuilder.EntitySet<Shared.Models.v1.CoreEffect>("CoreEffects");
        oDataBuilder.EntitySet<Shared.Models.v1.Effect>("Effects");
        EntitySetConfiguration<Shared.Models.v1.CharacterFeature> characterFeaturesConfiguration = oDataBuilder
            .EntitySet<Shared.Models.v1.CharacterFeature>("CharacterFeatures");
        characterFeaturesConfiguration.EntityType
            .HasKey((Shared.Models.v1.CharacterFeature characterFeature) =>
                new { characterFeature.CharacterId, characterFeature.FeatureId });
        return oDataBuilder.GetEdmModel();
    }
}

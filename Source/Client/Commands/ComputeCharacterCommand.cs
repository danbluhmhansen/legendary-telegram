namespace BlazorApp1.Client.Commands;

using System.Text.Json;
using System.Text.Json.Nodes;

using BlazorApp1.Shared.Models.v1;

using Json.Logic;
using Json.Patch;
using Json.Pointer;

public class ComputeCharacterCommand
{
    public JsonElement Execute(Character input)
    {
        JsonElement json = JsonDocument.Parse(new JsonObject
        {
            { "Strength", 8 },
            { "Dexterity", 8 },
            { "Constitution", 8 },
            { "Intelligence", 8 },
            { "Wisdom", 8 },
            { "Charisma", 8 },
            { "Defences", new JsonObject { { "Armour", 10 }, } },
        }.ToJsonString()).RootElement;

        foreach (CoreEffect effect in input.Effects.Where(effect => !string.IsNullOrWhiteSpace(effect.Rule)))
        {
            Rule? rule = JsonSerializer.Deserialize<Rule>(effect.Rule!);

            if (rule is null || string.IsNullOrWhiteSpace(effect.Path))
                continue;

            PatchResult patchResult = new JsonPatch(PatchOperation.Add(
                    JsonPointer.Parse(effect.Path), rule.Apply(json)))
                .Apply(json);
            json = patchResult.Result;
        }

        foreach (Effect effect in input.Features
            .SelectMany(feature => feature.Effects)
            .Where(effect => !string.IsNullOrWhiteSpace(effect.Rule)))
        {
            Rule? rule = JsonSerializer.Deserialize<Rule>(effect.Rule!);

            if (rule is null || string.IsNullOrWhiteSpace(effect.Path))
                continue;

            PatchResult patchResult = new JsonPatch(PatchOperation.Add(
                    JsonPointer.Parse(effect.Path), rule.Apply(json)))
                .Apply(json);
            json = patchResult.Result;
        }

        return json;
    }
}

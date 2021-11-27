using System.Text.Json;

using Json.Logic;
using Json.Schema;

JsonDocument doc = JsonDocument.Parse("{\"Strength\":8}");

Rule? rule = JsonSerializer.Deserialize<Rule>("{\"+\":[{\"var\":\"Strength\"},2]}");

if (rule is not null)
{
	JsonElement result = rule.Apply(doc.RootElement);
	Console.WriteLine(result.GetRawText());
}

using System.Text.Json.Nodes;

JsonNode tree = new JsonObject
{
	{
		"+",
		new JsonArray(
			new JsonObject
			{
				{
					"var",
					"Strength"
				}
			},
			2
		)
	}
};

if (tree is JsonObject o)
{
	Console.WriteLine("Object");
}
if (tree is JsonArray a)
{
	Console.WriteLine("Array");
}

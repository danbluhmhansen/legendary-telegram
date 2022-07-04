namespace LegendaryTelegram.Server.Models;

/// <summary>Represents a character.</summary>
public class Character
{
    /// <summary>The characters identifier.</summary>
    public Guid Id { get; set; }
    /// <summary>The characters name.</summary>
    public string Name { get; set; } = string.Empty;
}


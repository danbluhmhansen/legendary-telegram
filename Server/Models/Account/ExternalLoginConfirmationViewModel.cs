namespace LegendaryTelegram.Server.Models.Account;

using System.ComponentModel.DataAnnotations;

public class ExternalLoginConfirmationViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}

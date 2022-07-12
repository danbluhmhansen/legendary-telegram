using System.ComponentModel.DataAnnotations;

namespace LegendaryTelegram.Server.ViewModels.Account;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

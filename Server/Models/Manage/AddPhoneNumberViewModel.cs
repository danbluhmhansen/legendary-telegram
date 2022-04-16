namespace LegendaryTelegram.Server.Models.Manage;

using System.ComponentModel.DataAnnotations;

public class AddPhoneNumberViewModel
{
    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace LegendaryTelegram.Server.ViewModels.Manage;

public class AddPhoneNumberViewModel
{
    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
}

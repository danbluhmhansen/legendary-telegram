﻿namespace LegendaryTelegram.Server.Models.Account;

using System.ComponentModel.DataAnnotations;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}

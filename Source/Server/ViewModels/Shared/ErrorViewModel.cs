﻿namespace BlazorApp1.Server.ViewModels.Shared;

using System.ComponentModel.DataAnnotations;

public class ErrorViewModel
{
    [Display(Name = "Error")]
    public string? Error { get; set; }

    [Display(Name = "Description")]
    public string? ErrorDescription { get; set; }
}

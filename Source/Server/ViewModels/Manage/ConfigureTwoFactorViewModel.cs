namespace BlazorApp1.Server.ViewModels.Manage;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

public class ConfigureTwoFactorViewModel
{
	public string SelectedProvider { get; set; }

	public ICollection<SelectListItem> Providers { get; set; }
}

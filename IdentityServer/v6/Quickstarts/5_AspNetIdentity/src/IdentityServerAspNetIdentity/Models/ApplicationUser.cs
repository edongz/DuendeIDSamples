// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
	[PersonalData]
	[Display(Name = "真实姓名")]
	public string? Name { get; set; }
	[PersonalData]
	[Display(Name = "生日")]
	public DateTime DOB { get; set; }
}

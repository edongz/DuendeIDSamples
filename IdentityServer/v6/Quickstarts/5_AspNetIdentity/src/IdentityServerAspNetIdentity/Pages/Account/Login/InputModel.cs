// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Pages.Login;

public class InputModel
{

	[Display(Name = "用户名")]
	[Required(ErrorMessage = "用户名不能为空")]
	public string Username { get; set; }

	[Required]
	public string Password { get; set; }

	public bool RememberLogin { get; set; }

	public string ReturnUrl { get; set; }

	public string Button { get; set; }
}
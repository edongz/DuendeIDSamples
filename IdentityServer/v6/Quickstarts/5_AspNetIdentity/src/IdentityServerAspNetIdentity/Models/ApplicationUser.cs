<<<<<<< HEAD
﻿// Copyright (c) Duende Software. All rights reserved.
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

  [Display(Name = "电子邮件")]
  [EmailAddress]
  public override String Email { get; set; }
}
=======
﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;

namespace IdentityServerHost.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FavoriteColor { get; set; }
}
>>>>>>> 8fc72fb6f4a352a63567529630f4f5f49087fd7e

using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServerAspNetIdentity.Pages
{
  public class RolesModel : PageModel
  {
    public readonly ApplicationDbContext _db_context;
    private readonly ILogger<RolesModel> _logger;

    public RolesModel(ApplicationDbContext db_context, ILogger<RolesModel> logger)
    {
      _db_context = db_context;
      _logger = logger;
    }
    // Remaining API warnings ommited.
    public string ReturnUrl { get; set; }

    [BindProperty]
    public IdentityRole InputRole { get; set; }

    public void OnGet(string returnUrl = null)
    {
      ReturnUrl = returnUrl;
    }

    public IActionResult OnPost(string returnUrl = null)
    {
      InputRole.NormalizedName = InputRole.Name;
      _db_context.Roles.Add(InputRole);
      _db_context.SaveChanges();
      return LocalRedirect(Url.Content("~/"));
    }



  }
}

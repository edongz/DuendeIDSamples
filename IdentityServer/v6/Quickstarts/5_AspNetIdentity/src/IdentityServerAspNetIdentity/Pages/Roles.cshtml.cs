using IdentityServerAspNetIdentity.Data;
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

    public void OnGet()
    {
    }
  }
}

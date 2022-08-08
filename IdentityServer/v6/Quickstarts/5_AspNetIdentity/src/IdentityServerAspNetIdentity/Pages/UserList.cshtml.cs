using IdentityServerAspNetIdentity.Data;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAspNetIdentity.Pages
{
  public class UserListModel : PageModel
  {
    public readonly ApplicationDbContext _db_context;
    public UserListModel(ApplicationDbContext db_context)
    {
      _db_context = db_context;
    }

    public IList<ApplicationUser> LstUsers { get; set; }

    //打开时获取页面数据
    public async Task OnGetAsync()
    {
      LstUsers = await _db_context.Users.ToListAsync();
      ViewData["Title"] = "▷用户列表◁-";
    }
  }
}

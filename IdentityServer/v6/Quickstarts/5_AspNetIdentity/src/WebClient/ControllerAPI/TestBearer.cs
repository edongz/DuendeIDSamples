using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.ControllerAPI
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Cookies,Bearer")]

	public class TestBearer : ControllerBase
	{
		// /api/TestBearer/FindMaterialCode?MaterialCode=HDS69540M40
		[HttpGet(Name = "FindMaterialCode")]
		[Route("FindMaterialCode")]
		public string FindMaterialCode(string MaterialCode)
		{
			//DelixiIOrderContext deli=new DelixiIOrderContext
			string sx = MaterialCode + ">>>" + DateTime.Now.ToString();
			return sx;
		}

		[HttpGet]
		[Route("WhoamI")]
		public IActionResult WhoamI()
		{
			return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}
	}
}

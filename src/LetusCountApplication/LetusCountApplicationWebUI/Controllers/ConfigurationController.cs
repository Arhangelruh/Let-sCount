using Microsoft.AspNetCore.Mvc;

namespace LetusCountApplicationWebUI.Controllers
{
	public class ConfigurationController : Controller
	{
		/// <summary>
		/// Get main configuration page.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Configuration()
		{
			return View();
		}
	}
}

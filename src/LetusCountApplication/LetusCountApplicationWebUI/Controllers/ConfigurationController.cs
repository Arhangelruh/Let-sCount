using LetusCountApplication.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = Roles.Admin)]
		public IActionResult Configuration()
		{
			return View();
		}
	}
}

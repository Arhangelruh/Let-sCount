using Microsoft.AspNetCore.Mvc;

namespace LetusCountApplicationWebUI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

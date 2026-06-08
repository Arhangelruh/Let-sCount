using LetusCountApplication.Domain.Models;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LetusCountApplicationWebUI.Controllers
{
	public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
	{
		private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		private readonly SignInManager<User> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));

		/// <summary>
		/// Login model.
		/// </summary>
		/// <param name="returnUrl"></param>
		/// <returns>Login view</returns>
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		/// <summary>
		/// Login.
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Login result</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин и (или) пароль");
				}
			}
			return View(model);
		}

		/// <summary>
		/// Logout.
		/// </summary>
		/// <returns>Home view</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Change password model.
		/// </summary>
		/// <returns>User model</returns>
		[Authorize]
		public async Task<IActionResult> ChangePassword()
		{
			var username = HttpContext.User.Identity.Name;
			User user = await _userManager.FindByNameAsync(username);
			if (user == null)
			{
				return NotFound();
			}
			ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, UserName = user.UserName };
			return View(model);
		}

		/// <summary>
		/// Change password.
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Change password result</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var username = HttpContext.User.Identity.Name;
				User user = await _userManager.FindByNameAsync(username);

				if (user != null)
				{
					IdentityResult result =
						await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Пользователь не найден");
				}
			}
			return View(model);
		}

	}
}

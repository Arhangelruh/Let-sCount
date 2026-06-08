using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LetusCountApplicationWebUI.Controllers
{
	public class ProfileController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IProfileService profileService) : Controller
	{
		private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		private readonly IProfileService _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
		private readonly RoleManager<IdentityRole> _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

		/// <summary>
		/// Get profile
		/// </summary>
		/// <returns>Profile model</returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			var username = User.Identity.Name;
			if (username != null)
			{
				var user = await _userManager.FindByNameAsync(username);
				if (user != null)
				{

					var profile = await _profileService.GetProfileByUserIdAsync(user.Id);
					var model = new ProfileViewModel
					{
						FirstName = profile.FirstName,
						LastName = profile.LastName,
						MiddleName = profile.MiddleName,
					};
					return View(model);
				}
			}
			return View(new ProfileViewModel());
		}

		/// <summary>
		/// Profile model
		/// </summary>
		/// <returns>User model</returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditProfile()
		{
			var username = User.Identity.Name;

			if (username != null)
			{
				var user = await _userManager.FindByNameAsync(username);
				if (user != null)
				{
					var profile = await _profileService.GetProfileByUserIdAsync(user.Id);
					var model = new ProfileViewModel { FirstName = profile.FirstName, LastName = profile.LastName, MiddleName = profile.MiddleName };
					return View(model);
				}
			}
			ViewBag.ErrorMessage = "Не найден профиль пользователя.";
			ViewBag.ErrorTitle = "Ошибка";
			return View("~/Views/Error/Error.cshtml");
		}

		/// <summary>
		/// Edit user profile
		/// </summary>
		/// <param name="editProfile"></param>
		/// <returns>Result edit profile</returns>
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditProfile(ProfileViewModel editProfile)
		{
			var username = User.Identity.Name;
			if (username != null)
			{
				var user = await _userManager.FindByNameAsync(username);
				if (user != null)
				{
					var getProfile = await _profileService.GetProfileByUserIdAsync(user.Id);

					var profile = new ProfileDto
					{
						Id = getProfile.Id,
						FirstName = editProfile.FirstName,
						LastName = editProfile.LastName,
						MiddleName = editProfile.MiddleName,
					};

					await _profileService.EditProfileAsync(profile);
					return RedirectToAction("Profile");
				}
			}

			ViewBag.ErrorMessage = "Не найден профиль пользователя.";
			ViewBag.ErrorTitle = "Ошибка";
			return View("~/Views/Error/Error.cshtml");
		}

		/// <summary>
		/// Get profiles
		/// </summary>
		/// <returns>List profile model</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpGet]
		public async Task<IActionResult> UserProfiles()
		{
			var profiles = await _profileService.GetProfilesAsync();
			var models = new List<UserViewModel>();

			if (profiles.Count > 0)
			{
				foreach (var profile in profiles)
				{
					var user = await _userManager.FindByIdAsync(profile.UserId);

					if (user != null)
					{
						var ifAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin);
						string role;
						bool Locking = false;

						if (ifAdmin)
						{
							role = Roles.Admin;
						}
						else
						{
							role = Roles.User;
						}

						if (user.LockoutEnd >= DateTime.UtcNow)
						{
							Locking = true;
						}

						models.Add(
						new UserViewModel
						{
							Id = profile.Id,
							Login = user.UserName,
							FirstName = profile.FirstName,
							LastName = profile.LastName,
							MiddleName = profile.MiddleName,
							IsAdmin = role,
							Locking = Locking
						});
					}
				}
			}

			return View(models);
		}

		/// <summary>
		/// Model for create user account
		/// </summary>
		/// <returns>View model for create user</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpGet]
		public IActionResult CreateUser()
		{
			List<string> roles = new List<string>();

			foreach (var role in _roleManager.Roles)
			{
				roles.Add(role.Name);
			}
			roles.Reverse();
			ViewBag.roles = new SelectList(roles);
			return View();
		}

		/// <summary>
		/// Register
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Confirm email view</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateUser(CreateUserViewModel model)
		{
			List<string> roles = new List<string>();

			foreach (var role in _roleManager.Roles)
			{
				roles.Add(role.Name);
			}
			roles.Reverse();
			ViewBag.roles = new SelectList(roles);

			if (ModelState.IsValid)
			{
				var user = new User { UserName = model.Login };

				var searchUser = await _userManager.FindByNameAsync(model.Login);

				if (searchUser == null)
				{
					var result = await _userManager.CreateAsync(user, model.Password);

					if (result.Succeeded)
					{
						var profile = new ProfileDto()
						{
							UserId = user.Id,
							FirstName = model.FirstName,
							LastName = model.LastName,
							MiddleName = model.MiddleName
						};

						await _profileService.AddProfileAsync(profile);
						var addrole = await _userManager.AddToRoleAsync(user, model.IsAdmin);
						if (addrole.Succeeded)
						{
							return RedirectToAction("UserProfiles");
						}
						else
						{
							return View(model);
						}
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
					ViewBag.ErrorTitle = "Ошибка";
					ViewBag.ErrorMessage = "Пользователь с таким логином уже существует";
					return View("~/Views/Error/Error.cshtml");
				}
			}
			return View(model);
		}

		/// <summary>
		/// Lock user
		/// </summary>
		/// <param name="id"></param>
		/// <returns>result add to table</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpGet]
		public async Task<IActionResult> Lock(int Id)
		{
			await _profileService.LockAsync(Id);

			return RedirectToAction("UserProfiles");
		}

		/// <summary>
		/// Unlock user
		/// </summary>
		/// <param name="id"></param>
		/// <returns>result add to table</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpGet]
		public async Task<IActionResult> UnLock(int Id)
		{
			await _profileService.UnLockAsync(Id);

			return RedirectToAction("UserProfiles");
		}

		/// <summary>
		/// Get user
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Edituser model</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpGet]
		public async Task<IActionResult> EditUser(int Id)
		{
			var profile = await _profileService.GetProfileByIdAsync(Id);
			if (profile != null)
			{
				var user = await _userManager.FindByIdAsync(profile.UserId);
				if (user != null)
				{
					List<string> roles = new List<string>();

					foreach (var role in _roleManager.Roles)
					{
						roles.Add(role.Name);
					}
					roles.Reverse();

					ViewBag.roles = new SelectList(roles);

					var model = new EditUserViewModel
					{
						UserId = user.Id,
						Login = user.UserName,
						FirstName = profile.FirstName,
						LastName = profile.LastName,
						MiddleName = profile.MiddleName,
					};
					return View(model);
				}
			}
			ViewBag.ErrorTitle = "Ошибка";
			ViewBag.ErrorMessage = "Пользователь не найден.";
			return View("~/Views/Error/Error.cshtml");
		}

		/// <summary>
		/// Edit user
		/// </summary>
		/// <param name="editUser"></param>
		/// <returns>Result edit user account</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditUser(EditUserViewModel model)
		{
			List<string> roles = new List<string>();

			foreach (var role in _roleManager.Roles)
			{
				roles.Add(role.Name);
			}
			roles.Reverse();
			ViewBag.roles = new SelectList(roles);

			if (ModelState.IsValid)
			{
				var searchUser = await _userManager.FindByIdAsync(model.UserId);

				if (searchUser == null)
				{
					ViewBag.ErrorTitle = "Ошибка";
					ViewBag.ErrorMessage = "Пользователь не найден.";
					return View("~/Views/Error/Error.cshtml");
				}

				if (searchUser != null && searchUser.UserName != model.Login)
				{
					var exists = await _userManager.FindByNameAsync(model.Login);
					if (exists != null)
					{
						ViewBag.ErrorTitle = "Ошибка";
						ViewBag.ErrorMessage = "Пользователь с таким именем уже существует.";
						return View("~/Views/Error/Error.cshtml");
					}

					await _userManager.SetUserNameAsync(searchUser, model.Login);
				}

				if (model.Password != null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(searchUser);
					var changePassword = await _userManager.ResetPasswordAsync(searchUser, token, model.Password);
					if (!changePassword.Succeeded)
					{
						ModelState.AddModelError(nameof(model.Password), "Пароль не соответствует требованиям безопасности.");
						return View(model);
					}
				}

				var user = new UserDto
				{
					Id = model.UserId,
					Login = model.Login,
					FirstName = model.FirstName,
					LastName = model.LastName,
					MiddleName = model.MiddleName,
					Role = model.Role
				};
				await _profileService.EditUserAsync(user);
				return RedirectToAction("UserProfiles");
			}

			return View(model);
		}

		/// <summary>
		/// Delete user
		/// </summary>
		/// <param name="Id"></param>
		/// <returns>Result delete user account</returns>
		[Authorize(Roles = Roles.Admin)]
		[HttpGet]
		public async Task<ActionResult> DeleteUser(int id)
		{
			await _profileService.DeleteUserAsync(id);
			return RedirectToAction("UserProfiles");
		}
	}
}

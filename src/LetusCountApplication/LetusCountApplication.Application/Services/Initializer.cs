using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LetusCountApplication.Application.Services
{
	public class Initializer()
	{
		public static async Task InitializeAsync(IServiceProvider services)
		{
			var roleManager =
		   services.GetRequiredService<RoleManager<IdentityRole>>();

			var userManager =
				services.GetRequiredService<UserManager<User>>();

			if (!await roleManager.RoleExistsAsync(Roles.Admin))
			{
				await roleManager.CreateAsync(
					new IdentityRole(Roles.Admin));
			}

			if (!await roleManager.RoleExistsAsync(Roles.User))
			{
				await roleManager.CreateAsync(
					new IdentityRole(Roles.User));
			}

			var firstAdminLogin = "Admin";
			var firstAdminPassword = "Password123!";

			var checkAdmin = await userManager.FindByNameAsync(firstAdminLogin);

			if (checkAdmin == null)
			{

				var admin = new User { UserName = firstAdminLogin };

				var result = await userManager.CreateAsync(admin, firstAdminPassword);
				var profileService = services.GetRequiredService<IProfileService>();
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(admin, Roles.Admin);
					await profileService.AddProfileAsync(new ProfileDto { UserId = admin.Id });
				}
			}
		}
	}
}

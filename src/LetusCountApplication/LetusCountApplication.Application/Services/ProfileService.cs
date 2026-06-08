using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	public class ProfileService(IApplicationDbContext db, UserManager<User> userManager) : IProfileService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));
		private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

		public async Task AddProfileAsync(ProfileDto profile)
		{
			ArgumentNullException.ThrowIfNull(profile);

			var profileModel = new Profile
			{
				Id = profile.Id,
				UserId = profile.UserId,
				FirstName = profile.FirstName,
				LastName = profile.LastName,
				MiddleName = profile.MiddleName
			};

			_db.Profiles.Add(profileModel);
			await _db.SaveChangesAsync();
		}

		public async Task EditProfileAsync(ProfileDto profile)
		{
			ArgumentNullException.ThrowIfNull(profile);

			var getProfile = await _db.Profiles.FirstOrDefaultAsync(p => p.Id == profile.Id);

			if (getProfile != null)
			{
				getProfile.FirstName = profile.FirstName;
				getProfile.LastName = profile.LastName;
				getProfile.MiddleName = profile.MiddleName;

				await _db.SaveChangesAsync();
			}
		}

		public async Task<ProfileDto> GetProfileByIdAsync(int profileId)
		{
			var getProfile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == profileId);

			if (getProfile == null)
				return new ProfileDto();

			var profile = new ProfileDto
			{
				UserId = getProfile.UserId,
				FirstName = getProfile.FirstName,
				LastName = getProfile.LastName,
				MiddleName = getProfile.MiddleName,
				Id = getProfile.Id
			};

			return profile;
		}

		public async Task<ProfileDto> GetProfileByUserIdAsync(string userId)
		{
			ArgumentNullException.ThrowIfNull(userId);

			var getProfile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == userId);

			if (getProfile == null)
				return new ProfileDto();

			var profile = new ProfileDto
			{
				UserId = getProfile.UserId,
				FirstName = getProfile.FirstName,
				LastName = getProfile.LastName,
				MiddleName = getProfile.MiddleName,
				Id = getProfile.Id
			};

			return profile;
		}

		public async Task<List<ProfileDto>> GetProfilesAsync()
		{
			var profileDtos = new List<ProfileDto>();
			var profiles = await _db.Profiles.AsNoTracking().ToListAsync();

			if (profiles.Count == 0)
				return profileDtos;

			foreach (var profile in profiles)
			{
				profileDtos.Add(new ProfileDto
				{
					Id = profile.Id,
					UserId = profile.UserId,
					FirstName = profile.FirstName,
					LastName = profile.LastName,
					MiddleName = profile.MiddleName
				});
			}

			return profileDtos;
		}

		public async Task<bool> LockAsync(int profileId)
		{
			var getProfile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == profileId);
			if (getProfile != null)
			{
				var getUser = await _userManager.FindByIdAsync(getProfile.UserId);
				if (getUser != null)
				{
					getUser.LockoutEnd = DateTime.UtcNow.AddYears(200);
					await _userManager.UpdateAsync(getUser);
					return true;
				}
			}
			return false;
		}

		public async Task<bool> UnLockAsync(int profileId)
		{
			var getProfile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == profileId);
			if (getProfile != null)
			{
				var getUser = await _userManager.FindByIdAsync(getProfile.UserId);
				if (getUser != null)
				{
					getUser.LockoutEnd = DateTime.UtcNow.AddYears(-1);
					await _userManager.UpdateAsync(getUser);
					return true;
				}
			}
			return false;
		}

		public async Task<bool> DeleteUserAsync(int id)
		{
			var profile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
			if (profile != null)
			{
				var user = await _userManager.FindByIdAsync(profile.UserId);

				_db.Profiles.Remove(profile);
				await _db.SaveChangesAsync();

				if (user != null)
				{
					await _userManager.DeleteAsync(user);
				}
				return true;
			}
			return false;
		}

		public async Task EditUserAsync(UserDto user)
		{
			ArgumentNullException.ThrowIfNull(user);

			var searchUser = await _userManager.FindByIdAsync(user.Id);
			if (searchUser != null)
			{
				var searchProfile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == searchUser.Id);

				searchProfile.FirstName = user.FirstName;
				searchProfile.MiddleName = user.MiddleName;
				searchProfile.LastName = user.LastName;

				await _db.SaveChangesAsync();

				if (user.Role != null)
				{
					var checkRole = await _userManager.IsInRoleAsync(searchUser, user.Role);
					if (!checkRole)
					{
						var userRoles = await _userManager.GetRolesAsync(searchUser);
						foreach (var roles in userRoles)
						{
							await _userManager.RemoveFromRoleAsync(searchUser, roles);
						}
						await _userManager.AddToRoleAsync(searchUser, user.Role);
					}
				}
				searchUser.UserName = user.Login;

				await _userManager.UpdateAsync(searchUser);
			}
		}
	}
}

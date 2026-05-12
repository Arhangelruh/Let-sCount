using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models;

namespace LetusCountApplication.Application.Services
{
	public class ProfileService(IApplicationDbContext db) : IProfileService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task AddProfileAsync(Profile profile)
		{
			ArgumentNullException.ThrowIfNull(profile);

			_db.Profiles.Add(profile);
			await _db.SaveChangesAsync();
		}

		public async Task EditProfileAsync(ProfileDto profile)
		{
			ArgumentNullException.ThrowIfNull(profile);

			var getProfile = _db.Profiles.FirstOrDefault(p => p.Id == profile.Id);

			if (getProfile != null) {
				getProfile.FirstName = profile.FirstName;
				getProfile.LastName = profile.LastName;
				getProfile.MiddleName = profile.MiddleName;

				await _db.SaveChangesAsync();
			}
		}

		public async Task<ProfileDto> GetProfileById(int profileId)
		{
			var getProfile = _db.Profiles.FirstOrDefault( p => p.Id == profileId);

			if(getProfile == null) 
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

			var getProfile = _db.Profiles.FirstOrDefault(p => p.UserId == userId);

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
	}
}

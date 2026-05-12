using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Domain.Models;

namespace LetusCountApplication.Application.Interfaces
{
	/// <summary>
	/// Service to work with user profiles.
	/// </summary>
	public interface IProfileService
	{
		/// <summary>
		/// Add profile.
		/// </summary>
		/// <param name="profile">profile model</param>
		/// <returns></returns>
		Task AddProfileAsync(Profile profile);

		/// <summary>
		/// Edit profile.
		/// </summary>
		/// <param name="profile">Dto model</param>
		Task EditProfileAsync(ProfileDto profile);

		/// <summary>
		/// Get profile by user id.
		/// </summary>
		/// <param name="userId">Search profile by UserId key</param>
		Task<ProfileDto> GetProfileByUserIdAsync(string userId);

		/// <summary>
		/// Get profile by id
		/// </summary>
		/// <param name="profileId">profile id</param>
		/// <returns>profiledto</returns>
		Task<ProfileDto> GetProfileById(int profileId);
	}
}

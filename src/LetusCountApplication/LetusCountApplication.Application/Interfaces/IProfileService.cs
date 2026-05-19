using LetusCountApplication.Application.DTOModels;

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
		Task AddProfileAsync(ProfileDto profile);

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
		Task<ProfileDto> GetProfileByIdAsync(int profileId);

		/// <summary>
		/// Get all profiles.
		/// </summary>
		/// <returns>List of profiles</returns>
		Task<List<ProfileDto>> GetProfilesAsync();

		/// <summary>
		/// Lock user.
		/// </summary>
		/// <param name="profileId"></param>
		/// <returns>Bool result</returns>
		Task<bool> LockAsync(int profileId);

		/// <summary>
		/// Unlock.
		/// </summary>
		/// <param name="profileId"></param>
		/// <returns>Bool result</returns>
		Task<bool> UnLockAsync(int profileId);

		/// <summary>
		/// Delete user.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Bool result</returns>
		Task<bool> DeleteUserAsync(int id);

		/// <summary>
		/// Edit user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		Task EditUserAsync(UserDto user);
	}
}

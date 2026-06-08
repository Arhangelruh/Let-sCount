using Microsoft.AspNetCore.Identity;

namespace LetusCountApplication.Domain.Models
{
	public class User : IdentityUser
	{
		/// <summary>
		/// Navigation to Profile
		/// </summary>
		public Profile Profile { get; set; } = null!;
	}
}

namespace LetusCountApplication.Domain.Models
{
	public class Profile
	{
		/// <summary>
		/// 
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string UserId { get; set; } = null!;

		/// <summary>
		/// Navigation to User.
		/// </summary>
		public User User { get; set; } = null!;

		/// <summary>
		/// First name.
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// Name.
		/// </summary>
		public string? LastName { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		public string? MiddleName { get; set; }
	}
}

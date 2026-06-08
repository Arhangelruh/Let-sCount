using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class UserViewModel
	{
		/// <summary>
		/// Id. 
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Login. 
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// First name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Middle name.
		/// </summary>
		public string MiddleName { get; set; }		

		/// <summary>
		/// User role admin or no.
		/// </summary>
		public string IsAdmin { get; set; }

		/// <summary>
		/// Check account status
		/// </summary>
		public bool Locking { get; set; }
	}
}

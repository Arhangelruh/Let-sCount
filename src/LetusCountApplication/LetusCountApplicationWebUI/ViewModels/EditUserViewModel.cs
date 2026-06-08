using System.ComponentModel.DataAnnotations;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class EditUserViewModel
	{
		/// <summary>
		/// UserId. 
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Login. 
		/// </summary>
		[Display(Name = "Имя пользователя")]
		public string Login { get; set; }

		/// <summary>
		/// First name.
		/// </summary>
		[Display(Name = "Имя")]
		public string? FirstName { get; set; }

		/// <summary>
		/// Last name.
		/// </summary>
		[Display(Name = "Фамилия")]
		public string? LastName { get; set; }

		/// <summary>
		/// Middle name.
		/// </summary>
		[Display(Name = "Отчество")]
		public string? MiddleName { get; set; }

		/// <summary>
		/// User admin or no.
		/// </summary>
		public string Role { get; set; }

		/// <summary>
		/// Password
		/// </summary>
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 8)]
		[Display(Name = "Пароль")]
		public string? Password { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class CreateUserViewModel
	{
		/// <summary>
		/// Login. 
		/// </summary>
		[Required(ErrorMessage = "Имя пользователя не может быть пустым")]
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
		[Required(ErrorMessage = "Обновите страницу это поле не может быть пустым")]
		public string IsAdmin { get; set; }

		/// <summary>
		/// Password
		/// </summary>
		[Required(ErrorMessage = "Пароль не может быть пустым")]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 8)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		/// <summary>
		/// Confirm password
		/// </summary>
		[Required(ErrorMessage = "Введите подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }
	}
}

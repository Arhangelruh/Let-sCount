using LetusCountApplication.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class DepartmentViewModel
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Department name.
		/// </summary>
		[Required(ErrorMessage = "Наименование подразделения обязательно")]
		[StringLength(FieldLengthsConstants.MaxLengthShortMedium, ErrorMessage = "Длинна имени не должна быть более {1} символов")]
		public required string Name { get; set; }

		/// <summary>
		/// Department address.
		/// </summary>
		[StringLength(FieldLengthsConstants.MaxLengthLongMedium, ErrorMessage = "Длинна имени не должна быть более {1} символов")]
		public string? Address { get; set; }

		/// <summary>
		/// Department status.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Cash list.
		/// </summary>
		public List<CashViewModel>? Cashes { get; set; }
	}
}

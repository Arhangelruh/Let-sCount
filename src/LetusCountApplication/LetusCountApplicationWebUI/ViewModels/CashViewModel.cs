using LetusCountApplication.Domain.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class CashViewModel
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Cash desk name.
		/// </summary>
		[Required(ErrorMessage = "Наименование кассы обязательно")]
		[StringLength(FieldLengthsConstants.MaxLengthShortMedium, ErrorMessage = "Длинна имени не должна быть более {1} символов")]
		public required string Name { get; set; }

		/// <summary>
		/// Connection to department.
		/// </summary>
		public int DepartmentId { get; set; }

		/// <summary>
		/// Cash status.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Cash machine id.
		/// </summary>
		public int? SelectedCashMachineId { get; set; }

		/// <summary>
		/// Select list for cash machines.
		/// </summary>
		public List<SelectListItem> CashMachines { get; set; } = [];

		/// <summary>
		/// Cash machine.
		/// </summary>
		public CashMachineViewModel? CashMachine { get; set; }
	}
}

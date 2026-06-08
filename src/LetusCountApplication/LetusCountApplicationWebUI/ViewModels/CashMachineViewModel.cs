using LetusCountApplication.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class CashMachineViewModel
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Serial number.
		/// </summary>
		[Required(ErrorMessage = "Серийный номер обязателен")]
		[StringLength(FieldLengthsConstants.MaxLengthShort, ErrorMessage = "Длинна имени не должна быть более {1} символов")]
		public required string Serial { get; set; }

		/// <summary>
		/// Inventory number.
		/// </summary>
		[Required(ErrorMessage = "Инвентарный номер обязателен")]
		[StringLength(FieldLengthsConstants.MaxLengthShort, ErrorMessage = "Длинна имени не должна быть более {1} символов")]
		public required string Number { get; set; }

		/// <summary>
		/// Cash id.
		/// </summary>
		public int CashId { get; set; }

		/// <summary>
		/// Cash machine status.
		/// </summary>
		public bool IsActive { get; set; }


		/// <summary>
		/// Time when cash machine started work.
		/// </summary>
		public DateTime StartWorking { get; set; }

		/// <summary>
		/// Time when cash machine finished work.
		/// </summary>
		public DateTime? EndWorking { get; set; }
	}
}

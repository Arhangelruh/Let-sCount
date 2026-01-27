namespace LetusCountApplication.Domain.Models.EFModels
{
	public class CashCashMashine
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Cash identifier.
		/// </summary>
		public int CashId { get; set; }

		/// <summary>
		/// Navigate to Cash.
		/// </summary>
		public Cash Cash { get; set; }

		/// <summary>
		/// CashMashine identifier.
		/// </summary>
		public int CashMashineId { get; set; }

		/// <summary>
		/// Navigate to CashMachine.
		/// </summary>
		public CashMachine CashMachine { get; set; }

		/// <summary>
		/// Date time when cash mashine start to work.
		/// </summary>
		public DateTime StartWorking { get; set; }

		/// <summary>
		/// Date time when cash mashine finish working.
		/// </summary>
		public DateTime? EndWorking { get; set; }
	}
}

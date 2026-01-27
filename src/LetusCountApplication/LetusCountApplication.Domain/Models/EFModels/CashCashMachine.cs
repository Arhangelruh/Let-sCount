namespace LetusCountApplication.Domain.Models.EFModels
{
	public class CashCashMachine
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
		/// CashMachine identifier.
		/// </summary>
		public int CashMachineId { get; set; }

		/// <summary>
		/// Navigate to CashMachine.
		/// </summary>
		public CashMachine CashMachine { get; set; }

		/// <summary>
		/// Date time when cash machine start to work.
		/// </summary>
		public DateTime StartWorking { get; set; }

		/// <summary>
		/// Date time when cash machine finish working.
		/// </summary>
		public DateTime? EndWorking { get; set; }
	}
}

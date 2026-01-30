namespace LetusCountApplication.Domain.Models.EFModels
{
	public class CashMachine
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Serial number.
		/// </summary>
		public required string Serial { get; set; }

		/// <summary>
		/// Inventory number.
		/// </summary>
		public required int Number { get; set; }

		/// <summary>
		/// Navigation to CashCashMachine.
		/// </summary>
		public ICollection<CashCashMachine> CashCashMachine { get; set; }
	}
}

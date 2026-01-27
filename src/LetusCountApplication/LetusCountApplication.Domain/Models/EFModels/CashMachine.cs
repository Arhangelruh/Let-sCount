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
		/// Navigation to CashCashMashine.
		/// </summary>
		public ICollection<CashCashMashine> CashCashMashine { get; set; }
	}
}

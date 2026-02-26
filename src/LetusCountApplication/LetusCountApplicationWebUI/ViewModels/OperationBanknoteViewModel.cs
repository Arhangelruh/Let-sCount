namespace LetusCountApplicationWebUI.ViewModels
{
	public class OperationBanknoteViewModel
	{
		/// <summary>
		/// Banknote Serial number.
		/// </summary>
		public required string SerialNumber { get; set; }

		/// <summary>
		/// Denomination serial.
		/// </summary>
		public required string DenomName { get; set; }

		/// <summary>
		/// Banknote value.
		/// </summary>
		public int Value { get; set; }
	}
}

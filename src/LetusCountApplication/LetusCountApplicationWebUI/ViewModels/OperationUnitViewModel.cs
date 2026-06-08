namespace LetusCountApplicationWebUI.ViewModels
{
	public class OperationUnitViewModel
	{
		/// <summary>
		/// Operation id.
		/// </summary>
		public int OperationId { get; set; }

		/// <summary>
		/// Сurrency.
		/// </summary>
		public required string Currency { get; set; }

		/// <summary>
		/// Operation unit total sum.
		/// </summary>
		public decimal Sum {  get; set; }

		/// <summary>
		/// List of banknotes.
		/// </summary>
		public List<OperationBanknoteViewModel> operationBanknotes { get; set; }
	}
}

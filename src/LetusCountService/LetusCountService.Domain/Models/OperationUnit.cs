namespace LetusCountService.Domain.Models
{
	public class OperationUnit
	{
		/// <summary>
		/// Operation unit id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Unit currency.
		/// </summary>
		public required string Currency { get; set; }

		/// <summary>
		/// Operation identifier.
		/// </summary>
		public int OperationId { get; set; }

		/// <summary>
		/// Navigate to Operation.
		/// </summary>
		public Operation Operation { get; set; }

		/// <summary>
		/// Navigation to Banknotes.
		/// </summary>
		public ICollection<Banknote> Banknotes { get; set; }
	}
}

namespace LetusCountService.Domain.Models
{
	public class Banknote
	{
		/// <summary>
		/// Banknote id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Denomination serial.
		/// </summary>
		public required string DenomName { get; set; }

		/// <summary>
		/// Banknote value.
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// Banknote Serial number.
		/// </summary>
		public required string SerialNumber { get; set; }

		/// <summary>
		/// Operation unit identifer.
		/// </summary>
		public int OperationUnitId { get; set; }

		/// <summary>
		/// Navigate to OperationUnit.
		/// </summary>
		public required OperationUnit OperationUnit { get; set; }
	}
}

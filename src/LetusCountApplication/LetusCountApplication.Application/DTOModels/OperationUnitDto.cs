namespace LetusCountApplication.Application.DTOModels
{
	public class OperationUnitDto
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
		/// Total summ for unit.
		/// </summary>
		public decimal TotalSum { get; set; }

		/// <summary>
		/// Operation identifier.
		/// </summary>
		public int OperationId { get; set; }

		/// <summary>
		/// Banknotes.
		/// </summary>
		public required List<BanknoteDto> Banknotes { get; set; }
	}
}

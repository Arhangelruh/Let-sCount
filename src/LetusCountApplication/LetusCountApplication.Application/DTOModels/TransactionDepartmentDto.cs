namespace LetusCountApplication.Application.DTOModels
{
	public class TransactionDepartmentDto
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Department name.
		/// </summary>
		public required string Name { get; set; }

		/// <summary>
		/// Department address.
		/// </summary>
		public string? Address { get; set; }

		/// <summary>
		/// Department status.
		/// </summary>
		public required bool IsActive { get; set; }

		/// <summary>
		/// Cash.
		/// </summary>
		public CashDto Cashe { get; set; }
	}
}

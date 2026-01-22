namespace LetusCountApplication.Domain.Models.EFModels
{
	public class Department
	{
		/// <summary>
		/// Banknote id.
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
		///  Navigate to Operation units.
		/// </summary>
		public ICollection<Cash> Cashes { get; set; }
	}
}

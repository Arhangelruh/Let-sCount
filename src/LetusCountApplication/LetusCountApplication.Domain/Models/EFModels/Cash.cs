namespace LetusCountApplication.Domain.Models.EFModels
{
	public class Cash
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Cash desk name.
		/// </summary>
		public required string Name { get; set; }

		/// <summary>
		/// Kisan number.
		/// </summary>
		public required string Kisan { get; set; }

		/// <summary>
		/// Department identifier.
		/// </summary>
		public int DepartmentId { get; set; }

		/// <summary>
		/// Navigate to Operation.
		/// </summary>
		public Department Department { get; set; }
	}
}

namespace LetusCountApplication.Application.DTOModels
{
	public class CashDto
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
		/// Connection to department.
		/// </summary>
		public required int DepartmentId { get; set; }

		/// <summary>
		/// Cash status.
		/// </summary>
		public required bool IsActive {  get; set; }
	}
}

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
	}
}

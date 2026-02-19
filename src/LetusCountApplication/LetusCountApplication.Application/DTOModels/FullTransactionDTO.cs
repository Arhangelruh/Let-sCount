namespace LetusCountApplication.Application.DTOModels
{
	public class FullTransactionDTO
	{
		/// <summary>
		/// Department.
		/// </summary>
		public DepartmentDto Department { get; set; }

		/// <summary>
		/// Banknote.
		/// </summary>
		public FullBanknoteDto Banknote { get; set;}
	}
}

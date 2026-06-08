namespace LetusCountApplication.Application.DTOModels
{
	public class ShortOperationInformationDto
	{
		/// <summary>
		/// Department information.
		/// </summary>
		public TransactionDepartmentDto Department { get; set; }

		/// <summary>
		/// Operation information.
		/// </summary>
		public TransactionOperationDto Operation { get; set; }
	}
}

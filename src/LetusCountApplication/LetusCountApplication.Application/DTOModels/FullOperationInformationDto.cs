namespace LetusCountApplication.Application.DTOModels
{
	public class FullOperationInformationDto
	{
		/// <summary>
		/// Department information.
		/// </summary>
		public TransactionDepartmentDto Department { get; set; }

		/// <summary>
		/// Operation information.
		/// </summary>
		public OperationDto Operation { get; set; }
	}
}

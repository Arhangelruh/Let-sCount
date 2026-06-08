namespace LetusCountApplication.Application.DTOModels
{
	public class CashTransactionInformationDto
	{
		public CashDto Cash { get; set; }

		/// <summary>
		/// Operation information.
		/// </summary>
		public TransactionOperationDto Operation { get; set; }
	}
}

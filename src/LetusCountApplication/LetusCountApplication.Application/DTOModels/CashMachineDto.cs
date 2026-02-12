namespace LetusCountApplication.Application.DTOModels
{
	public class CashMachineDto
	{
		/// <summary>
		/// Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Serial number.
		/// </summary>
		public required string Serial { get; set; }

		/// <summary>
		/// Inventory number.
		/// </summary>
		public required string Number {  get; set; }

		/// <summary>
		/// Cash id.
		/// </summary>
		public int CashId {  get; set; }

		/// <summary>
		/// Time when cash machine started work.
		/// </summary>
		public DateTime? StartWorking {  get; set; }

		/// <summary>
		/// Time when cash machine finished work.
		/// </summary>
		public DateTime? EndWorking { get; set; }
	}
}

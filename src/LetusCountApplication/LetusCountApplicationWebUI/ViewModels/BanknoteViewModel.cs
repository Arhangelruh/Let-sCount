namespace LetusCountApplicationWebUI.ViewModels
{
	public class BanknoteViewModel
	{
		/// <summary>
		/// Department name.
		/// </summary>
		public string Department {  get; set; }

		/// <summary>
		/// Cash name.
		/// </summary>
		public string Cash {  get; set; }

		/// <summary>
		/// Denomination serial.
		/// </summary>
		public required string DenomName { get; set; }

		/// <summary>
		/// Banknote value.
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// Banknote Serial number.
		/// </summary>
		public required string SerialNumber { get; set; }

		/// <summary>
		/// Сurrency.
		/// </summary>
		public required string Currency { get; set; }

		/// <summary>
		/// Operation identifier.
		/// </summary>
		public int OperationId { get; set; }

		/// <summary>
		/// Machine Serial number.
		/// </summary>
		public required string MachineSerial { get; set; }

		/// <summary>
		/// Start operation time.
		/// </summary>
		public DateTimeOffset StartTime { get; set; }

		/// <summary>
		/// End operation time.
		/// </summary>
		public DateTimeOffset EndTime { get; set; }
	}
}

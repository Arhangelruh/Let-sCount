namespace LetusCountApplicationWebUI.ViewModels
{
	public class OperationViewModel
	{
		/// <summary>
		/// Department name.
		/// </summary>
		public string? Department { get; set; }

		/// <summary>
		/// Cash name.
		/// </summary>
		public string Cash { get; set; }

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

		/// <summary>
		/// Operation units.
		/// </summary>
		public List<OperationUnitViewModel>? OperationUnits { get; set; }
	}
}

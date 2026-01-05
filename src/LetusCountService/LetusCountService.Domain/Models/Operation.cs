namespace LetusCountService.Domain.Models
{
	public class Operation
	{
		/// <summary>
		/// Operation Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Machine Serial number.
		/// </summary>
		public required string MachineSerial { get; set; }

		/// <summary>
		/// Start operation time.
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// End operation time.
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		///  Navigate to Operation units.
		/// </summary>
		public required ICollection<OperationUnit> OperationUnits { get; set; }
	}
}

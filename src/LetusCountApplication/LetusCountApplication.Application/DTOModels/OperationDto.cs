namespace LetusCountApplication.Application.DTOModels
{
	public class OperationDto
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
		public DateTimeOffset StartTime { get; set; }

		/// <summary>
		/// End operation time.
		/// </summary>
		public DateTimeOffset EndTime { get; set; }

		/// <summary>
		/// Operation Units.
		/// </summary>
		public required List<OperationUnitDto> OperationUnits { get; set; }
	}
}

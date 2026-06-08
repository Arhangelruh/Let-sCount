namespace LetusCountApplication.Application.QueryModels
{
	public class CashOperationQuery
	{
		/// <summary>
		/// Department id.
		/// </summary>
		public int CashId { get; set; }

		/// <summary>
		/// DateTime from.
		/// </summary>
		public DateTimeOffset DateFrom { get; set; }

		/// <summary>
		/// DateTime To
		/// </summary>
		public DateTimeOffset DateTo { get; set; }

		/// <summary>
		/// Sort field.
		/// </summary>
		public string? SortField { get; set; }

		/// <summary>
		/// Sort direction.
		/// </summary>
		public string? SortDirection { get; set; }

		/// <summary>
		/// Page number.
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Page size.
		/// </summary>
		public int PageSize { get; set; }
	}
}

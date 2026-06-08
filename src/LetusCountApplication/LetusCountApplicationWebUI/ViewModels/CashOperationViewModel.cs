using LetusCountApplication.Application.Services;

namespace LetusCountApplicationWebUI.ViewModels
{
	public class CashOperationViewModel
	{
		/// <summary>
		/// Date and time from, for search.
		/// </summary>
		public DateTime DateTimeFrom { get; set; } = DateTime.Now.AddDays(-1);

		/// <summary>
		/// Date and time to, for search.
		/// </summary>
		public DateTime DateTimeTo { get; set; } = DateTime.Now;

		/// <summary>
		/// Department id.
		/// </summary>
		public int CashId { get; set; }

		/// <summary>
		/// Cash name.
		/// </summary>
		public string Cash { get; set; }

		/// <summary>
		/// Department name.
		/// </summary>
		public string Department { get; set; }

		/// <summary>
		/// Sort field.
		/// </summary>
		public string SortField { get; set; } = "EndTime";

		/// <summary>
		/// Sort direction.
		/// </summary>
		public string SortDirection { get; set; } = "desc";

		/// <summary>
		/// Page number.
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Page size.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Paged list of operations.
		/// </summary>
		public PagedResult<OperationViewModel> Operations { get; set; }

		public string GetNextSortDirection(string field)
		{
			if (SortField == field && SortDirection == "asc")
				return "desc";

			return "asc";
		}
	}
}

namespace LetusCountApplication.Application.Services
{
	public class PagedResult<T>
	{
		/// <summary>
		/// List of items.
		/// </summary>
		public IReadOnlyList<T> Items { get; }

		/// <summary>
		/// Total count.
		/// </summary>
		public int TotalCount { get; }

		/// <summary>
		/// Page number.
		/// </summary>
		public int Page { get; }

		/// <summary>
		/// Page size.
		/// </summary>
		public int PageSize { get; }

		/// <summary>
		/// Total pages.
		/// </summary>
		public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

		public bool HasPreviousPage => Page > 1;

		public bool HasNextPage => Page < TotalPages;

		public PagedResult(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
		{
			Items = items;
			TotalCount = totalCount;
			Page = page;
			PageSize = pageSize;
		}
	}
}

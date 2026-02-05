using LetusCountApplication.Application.DTOModels;

namespace LetusCountApplication.Application.Interfaces
{
	public interface IOperationService
	{
		/// <summary>
		/// Get operations.
		/// </summary>
		/// <param name="begin">Date from</param>
		/// <param name="end">Date to</param>
		/// <param name="serialNumber">Cash machine serial number</param>
		/// <param name="pageIndex">page index</param>
		/// <param name="pageSize">page size</param>
		/// <returns></returns>
		Task<List<OperationDto>> GetOperationsByMachine(DateTime begin, DateTime end, string serialNumber, int pageIndex, int pageSize);

		/// <summary>
		/// Get amount of counts.
		/// </summary>
		/// <param name="begin">Date from</param>
		/// <param name="end">Date to</param>
		/// <param name="serialNumber">Cash machine serial number</param>
		/// <returns></returns>
		Task<int> OperationCountsAsync(DateTime begin, DateTime end, string serialNumber);
	}
}

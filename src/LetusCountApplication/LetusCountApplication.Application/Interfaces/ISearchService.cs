using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.QueryModels;
using LetusCountApplication.Application.Services;

namespace LetusCountApplication.Application.Interfaces
{
	public interface ISearchService
	{
		/// <summary>
		/// Get information about operations by banknote.
		/// </summary>
		/// <param name="serialNumber"></param>
		/// <returns></returns>
		Task<List<FullTransactionDTO>> SearchByBanknoteAsync(string serialNumber);

		/// <summary>
		/// Get information about operation.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<FullOperationInformationDto> GetOperationInformationAsync(int id);

		/// <summary>
		/// Get paged information by department between two dates.
		/// </summary>
		/// <param name="query">query params</param>
		/// <returns>Paged list ShortOperationInformationDto</returns>
		Task<PagedResult<ShortOperationInformationDto>> SearchByDepartment(DepartmentOperationQuery query);
	}
}

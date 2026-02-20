using LetusCountApplication.Application.DTOModels;

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
	}
}

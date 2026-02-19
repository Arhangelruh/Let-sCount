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
		Task<List<FullTransactionDTO>> SearchByBanknote(string serialNumber);
	}
}

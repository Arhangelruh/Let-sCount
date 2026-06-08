using LetusCountApplication.Application.DTOModels;

namespace LetusCountApplication.Application.Interfaces
{
	public interface IBanknotesService
	{
		/// <summary>
		/// Get banknotes by serial number.
		/// </summary>
		/// <param name="serialNumber">Serial number</param>
		/// <returns>List of banknotes</returns>
		Task<List<FullBanknoteDto>> GetBySerialAsync(string serialNumber);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="operationUnitId">OperationUnit Id</param>
		/// <returns>List of banknotes</returns>
		Task<List<BanknoteDto>> GetByOperationUnitAsync(int operationUnitId);
	}
}

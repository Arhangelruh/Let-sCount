using LetusCountApplication.Application.DTOModels;

namespace LetusCountApplication.Application.Interfaces
{
	public interface ICashesService
	{
		/// <summary>
		/// Add cash.
		/// </summary>
		/// <param name="cashDto">Cash dto model</param>		
		Task AddCashAsync(CashDto cashDto);

		/// <summary>
		/// Delete cash.
		/// </summary>
		/// <param name="cashDto">Cash dto model</param>		
		Task DeleteCashAsync(CashDto cashDto);

		/// <summary>
		/// Edit cash.
		/// </summary>
		/// <param name="cashDto">Cash dto model</param>
		Task EditCashAsync(CashDto cashDto);

		/// <summary>
		/// Get cash by id.
		/// </summary>
		/// <param name="id">Cash id</param>
		/// <returns>Cash dto model</returns>
		Task<CashDto> GetCashByIdAsync(int id);

		/// <summary>
		/// Get cash by department.
		/// </summary>
		/// <param name="id">Department id</param>
		/// <returns></returns>
		Task<List<CashDto>> GetCashesByDepartmentAsync(int id);

		/// <summary>
		/// Add cash machine to cash.
		/// </summary>
		/// <param name="cashDto">Cash dto model</param>
		/// <returns></returns>
		Task AddCashMachineToCashAsync(CashMachineDto cashMachineDto);
	}
}

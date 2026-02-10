using LetusCountApplication.Application.DTOModels;

namespace LetusCountApplication.Application.Interfaces
{
	public interface ICashMachinesService
	{
		/// <summary>
		/// Add cash machine.
		/// </summary>
		/// <param name="cashMachineDto">CashMachine model</param>
		/// <returns></returns>
		Task AddCashMachineAsync(CashMachineDto cashMachineDto);

		/// <summary>
		/// Delete cash machine.
		/// </summary>
		/// <param name="cashMachineId">CashMachine id</param>
		/// <returns></returns>
		Task<bool> DeleteCashMachineAsync(int cashMachineId);

		/// <summary>
		/// Get cash machine by id.
		/// </summary>
		/// <param name="cashMachineId">CashMachine id</param>
		/// <returns>CashMachine dto model</returns>
		Task <CashMachineDto>? GetCashMachineByIdAsync(int cashMachineId);

		/// <summary>
		/// Get cash machines by cash id.
		/// </summary>
		/// <param name="cashId"></param>
		/// <returns>List of cash machines</returns>
		Task<List<CashMachineDto>> GetCashMashinesByCashAsync(int cashId);

		/// <summary>
		/// Get all cash machines.
		/// </summary>
		/// <returns>List of cash machines</returns>
		Task<List<CashMachineDto>> GetAllCashMachinesAsync();

		/// <summary>
		/// Get cash machines by serial or number.
		/// </summary>
		/// <param name="serial">Serial number</param>
		/// <param name="number">Inventory number</param>
		/// <returns>List of cash machines</returns>
		Task<List<CashMachineDto>> GetCashMachineBySerialOrNumberAsync(string serial, string number);
	}
}

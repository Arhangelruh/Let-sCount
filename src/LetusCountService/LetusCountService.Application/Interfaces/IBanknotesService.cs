using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Interfaces
{
	public interface IBanknotesService
	{
		/// <summary>
		/// Add banknote.
		/// </summary>
		/// <param name="banknote">Banknote model</param>
		/// <returns></returns>
		Task AddBanknoteAsync(Banknote banknote);
	}
}

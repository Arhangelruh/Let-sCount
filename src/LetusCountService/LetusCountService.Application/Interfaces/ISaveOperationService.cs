using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Interfaces
{
	public interface ISaveOperationService
	{
		/// <summary>
		/// Save operation.
		/// </summary>
		/// <param name="operation">Operation model</param>
		/// <returns></returns>
		Task SaveOperationAsync(Operation operation);
	}
}

using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Interfaces
{
	public interface IOperationService
	{
		/// <summary>
		/// Add operation.
		/// </summary>
		/// <param name="operation">Operation model</param>
		/// <returns></returns>
		Task<int> AddOperationAsync(Operation operation);
	}
}

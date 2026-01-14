using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Interfaces
{
	public interface IOperationUnitService
	{
		/// <summary>
		/// Add operation unit.
		/// </summary>
		/// <param name="operationUnit">OperationUnit model</param>
		/// <returns></returns>
		Task<int> AddOperationUnitAsync(OperationUnit operationUnit);
	}
}

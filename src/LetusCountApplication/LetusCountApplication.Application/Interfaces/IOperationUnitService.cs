using LetusCountApplication.Application.DTOModels;

namespace LetusCountApplication.Application.Interfaces
{
	public interface IOperationUnitService
	{
		/// <summary>
		/// Get OperationUnit by id.
		/// </summary>
		/// <param name="id">Operatiom unit id</param>
		/// <returns>Operatiom unit dto model</returns>
		Task<OperationUnitDto>? GetOperatiomUnitById(int id);

		/// <summary>
		/// Get OperationUnits by operation.
		/// </summary>
		/// <param name="operationId">Operation Id</param>
		/// <returns>List of OperationUnit</returns>
		Task<List<OperationUnitDto>> GetOperationUnitsByOperation(int operationId);
	}
}

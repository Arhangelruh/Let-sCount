using LetusCountApplication.Application.DTOModels;

namespace LetusCountApplication.Application.Interfaces
{
	public interface IDepartmentsService
	{
		/// <summary>
		/// Add department.
		/// </summary>
		/// <param name="department">Dto model</param>		
		Task AddDepartmentAsync(DepartmentDto department);

		/// <summary>
		/// Delete department.
		/// </summary>
		/// <param name="department">Dto model</param>		
	    Task DeleteDepartmentAsync(DepartmentDto department);

		/// <summary>
		/// Update department.
		/// </summary>
		/// <param name="department">dto model</param>		
		Task UpdateDepartmentAsync(DepartmentDto department);

		/// <summary>
		/// Get department by id.
		/// </summary>
		/// <param name="departmentId">department id</param>
		/// <returns>dto model</returns>
		Task<DepartmentDto> GetDepartmentByIdAsync(DepartmentDto departmentId);

		/// <summary>
		/// Get all departments.
		/// </summary>
		/// <returns>Departments list</returns>
		Task <List<DepartmentDto>> GetAllDepartmentsAsync();
	}
}

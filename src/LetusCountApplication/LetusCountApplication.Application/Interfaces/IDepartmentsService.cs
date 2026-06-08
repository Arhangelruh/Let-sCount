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
	    Task<bool> DeleteDepartmentAsync(int departmentId);

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
		Task<DepartmentDto>? GetDepartmentByIdAsync(int departmentId);

		/// <summary>
		/// Get all active departments.
		/// </summary>
		/// <returns>List of active departments</returns>
		Task<List<DepartmentDto>> GetAllActiveDepartmentsAsync();

		/// <summary>
		/// Get all departments.
		/// </summary>
		/// <returns>Departments list</returns>
		Task <List<DepartmentDto>> GetAllDepartmentsAsync();

		/// <summary>
		/// Change department status if it were closed or reopen.
		/// </summary>
		/// <param name="id">Department id</param>
		/// <returns></returns>
		Task ChangeDepartmentStatusAsync(int id);
	}
}

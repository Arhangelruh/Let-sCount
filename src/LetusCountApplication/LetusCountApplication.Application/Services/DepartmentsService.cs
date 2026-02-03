using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	/// <inheritdoc/>
	public class DepartmentsService(IApplicationDbContext db, ICashesService cashesService) : IDepartmentsService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));
		private readonly ICashesService _cashesService = cashesService ?? throw new ArgumentNullException(nameof(cashesService));

		public async Task AddDepartmentAsync(DepartmentDto department)
		{
			ArgumentNullException.ThrowIfNull(department);

			_db.Departments.Add(new Department
			{
				Name = department.Name,
				Address = department.Address,
				WorkStatus = true
			});
			await _db.SaveChangesAsync();
		}

		public async Task<bool> DeleteDepartmentAsync(int departmentId)
		{			
			var department = _db.Departments.FirstOrDefault(dep=>dep.Id == departmentId);
			if (department != null) {
				var cashes = await _cashesService.GetCashesByDepartmentAsync(departmentId);
				if (cashes.Count > 0)
				{
					foreach(var cash in cashes)
					{
						var result = await _cashesService.DeleteCashAsync(cash);
						if(!result)
							return false;
					}
				}
				_db.Departments.Remove(department);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<DepartmentDto>> GetAllActiveDepartmentsAsync()
		{
			var departments = await _db.Departments
				.AsNoTracking()
				.Where(dep=>dep.WorkStatus == true)
				.Select(dep=>new DepartmentDto { 
					Id = dep.Id,
					Name = dep.Name,
					Address = dep.Address,
					IsActive = dep.WorkStatus
				})
				.ToListAsync();

			return departments;
		}

		public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
		{
			var departments = await _db.Departments
				.AsNoTracking()				
				.Select(dep => new DepartmentDto
				{
					Id = dep.Id,
					Name = dep.Name,
					Address = dep.Address,
					IsActive = dep.WorkStatus
				})
				.ToListAsync();

			return departments;
		}

		public async Task<DepartmentDto>? GetDepartmentByIdAsync(int departmentId)
		{
			var department = await _db.Departments
				.AsNoTracking()
				.Where(dep => dep.Id == departmentId)
				.Select(dep => new DepartmentDto
				{
					Id = dep.Id,
					Name = dep.Name,
					Address = dep.Address,
					IsActive = dep.WorkStatus
				})
				.FirstOrDefaultAsync();
			return department;
		}

		public async Task UpdateDepartmentAsync(DepartmentDto department)
		{
			ArgumentNullException.ThrowIfNull(department);

			var getDepartment = await _db.Departments
				.FirstOrDefaultAsync(dep=>dep.Id == department.Id);

			if(getDepartment != null)
			{
				getDepartment.Name = department.Name;
				getDepartment.Address = department.Address;

				await _db.SaveChangesAsync();
			}
		}

		public async Task ChangeDepartmentStatusAsync(int id)
		{
			var dep = await _db.Departments.FirstOrDefaultAsync(c => c.Id == id);
			if (dep != null)
			{
				dep.WorkStatus = !dep.WorkStatus;
				await _db.SaveChangesAsync();
			}
		}
	}
}

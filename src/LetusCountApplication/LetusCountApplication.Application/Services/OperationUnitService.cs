using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	public class OperationUnitService(IApplicationDbContext db) : IOperationUnitService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<OperationUnitDto>? GetOperatiomUnitById(int id)
		{
			var operationUnit = await _db.OperationUnits
				.AsNoTracking()
				.Where(o => o.Id == id)
				.Select(o=> new OperationUnitDto
				{
					Id = o.Id,
					Currency = o.Currency,
					OperationId = o.OperationId
				})
				.FirstOrDefaultAsync();

			return operationUnit;
		}

		public async Task<List<OperationUnitDto>> GetOperationUnitsByOperation(int operationId)
		{
			var operationUnits = await _db.OperationUnits
				.AsNoTracking()
				.Where(o=>o.OperationId == operationId)
				.Select(o=> new OperationUnitDto
				{
					Id = o.Id,
					Currency= o.Currency,
					OperationId = o.OperationId
				})
				.ToListAsync();

			return operationUnits;
		}
	}
}

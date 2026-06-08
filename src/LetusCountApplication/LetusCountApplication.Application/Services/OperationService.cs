using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	public class OperationService(IApplicationDbContext db) : IOperationService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<List<OperationDto>> GetOperationsByMachine(DateTime begin, DateTime end, string serialNumber, int pageIndex, int pageSize)
		{
			var operationPage = await _db.Operations
			 .AsNoTracking()
			 .Where(o => o.MachineSerial == serialNumber && o.StartTime >= begin && o.EndTime <= end)
			 .OrderByDescending(o => o.EndTime)
			 .Skip(pageIndex * pageSize)
			 .Take(pageSize)
			 .Select(o => new
			 {
				 o.Id,
				 o.StartTime,
				 o.EndTime,
				 o.MachineSerial
			 })
			 .ToListAsync();

			var orderMap = operationPage
			   .Select((x, index) => new { x.Id, index })
			   .ToDictionary(x => x.Id, x => x.index);

			var operationIds = operationPage.Select(x => x.Id).ToList();

			var operations = await _db.Operations
				.AsNoTracking()
				.Where(o => operationIds.Contains(o.Id))
				.Select(o => new OperationDto
				{
					Id = o.Id,
					MachineSerial = o.MachineSerial,
					StartTime = o.StartTime,
					EndTime = o.EndTime,
					OperationUnits = o.OperationUnits.Select(ou => new OperationUnitDto
					{
						Id = ou.Id,
						Currency = ou.Currency,
						Banknotes = ou.Banknotes.Select(b => new BanknoteDto
						{
							Id = b.Id,
							SerialNumber = b.SerialNumber,
							DenomName = b.DenomName,
							Value = b.Value
						}).ToList()
					}).ToList()
				})
				.ToListAsync();

			var orderedResult = operations
				.OrderBy(o => orderMap[o.Id])
				.ToList();

			return orderedResult;
		}


		public async Task<int> OperationCountsAsync(DateTime begin, DateTime end, string serialNumber)
		{
			return await _db.Operations
			  .AsNoTracking()
			  .Where(o => o.MachineSerial == serialNumber
					&& o.StartTime >= begin
					&& o.EndTime <= end)
			   .CountAsync();
		}

		public async Task<OperationDto> GetOperationByIdAsync(int id) {

			var operation = await _db.Operations
				.AsNoTracking()
				.Where(o => o.Id == id)
				.Select(o => new OperationDto
				{
					Id = o.Id,
					MachineSerial = o.MachineSerial,
					StartTime = o.StartTime,
					EndTime = o.EndTime,
					OperationUnits = o.OperationUnits.Select(ou => new OperationUnitDto
					{
						Id = ou.Id,
						Currency = ou.Currency,
						TotalSum = ou.TotalSum,
						Banknotes = ou.Banknotes.Select(b => new BanknoteDto
						{
							Id = b.Id,
							SerialNumber = b.SerialNumber,
							DenomName = b.DenomName,
							Value = b.Value
						}).ToList()
					}).ToList()
				})
				.FirstOrDefaultAsync();

			return operation;
		}
	}
}

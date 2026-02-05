using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	public class BanknotesService(IApplicationDbContext db) : IBanknotesService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<List<BanknoteDto>> GetByOperationUnitAsync(int operationUnitId)
		{
			var banknotes = await _db.Banknotes
				.AsNoTracking()
				.Where(b => b.OperationUnitId == operationUnitId)
				.Select(b => new BanknoteDto
				{
					Id = b.Id,
					SerialNumber = b.SerialNumber,
					DenomName = b.DenomName,
					Value = b.Value,
					OperationUnitId = b.OperationUnitId
				})
				.ToListAsync();			

			return banknotes;
		}

		public async Task<List<FullBanknoteDto>> GetBySerialAsync(string serialNumber)
		{
			var banknotes = await _db.Banknotes
				.AsNoTracking()
				.Where(b => b.SerialNumber == serialNumber)
				.Select(b => new FullBanknoteDto
				{
					Id = b.Id,
					SerialNumber = b.SerialNumber,
					DenomName = b.DenomName,
					Value = b.Value,
					Currency = b.OperationUnit.Currency,
					OperationId = b.OperationUnit.OperationId,
					MachineSerial = b.OperationUnit.Operation.MachineSerial,
					StartTime = b.OperationUnit.Operation.StartTime,
					EndTime = b.OperationUnit.Operation.EndTime
				})
				.ToListAsync();
			return banknotes;
		}
	}
}

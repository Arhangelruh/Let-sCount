using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	/// <inheritdoc/>
	public class CashMachinesService(IApplicationDbContext db) : ICashMachinesService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task AddCashMachineAsync(CashMachineDto cashMachineDto)
		{
			ArgumentNullException.ThrowIfNull(cashMachineDto);

			var newMachine = new CashMachine
			{
				Serial = cashMachineDto.Serial,
				Number = cashMachineDto.Number
			};

			_db.CashMachines.Add(newMachine);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> DeleteCashMachineAsync(int cashMachineId)
		{
			bool result = false;

			var cashMachine = await _db.CashMachines.AsNoTracking().FirstOrDefaultAsync(cm => cm.Id == cashMachineId);
			if (cashMachine != null)
			{
				var checChain = await _db.CashCashMachines.AsNoTracking().FirstOrDefaultAsync(cm => cm.CashMachineId == cashMachine.Id);
				if (checChain == null)
				{
					_db.CashMachines.Remove(cashMachine);
					await _db.SaveChangesAsync();
					result = true;
				}
			}
			return result;
		}

		public async Task<CashMachineDto>? GetCashMachineByIdAsync(int cashMachineId)
		{
			var cashMachine = await _db.CashMachines
				.AsNoTracking()
				.Where(cm => cm.Id == cashMachineId)
				.Select(cm => new CashMachineDto
				{
					Id = cm.Id,
					Serial = cm.Serial,
					Number = cm.Number
				})
				.FirstOrDefaultAsync();
			return cashMachine;
		}

		public async Task<List<CashMachineDto>> GetCashMashinesByCashAsync(int cashId)
		{
			List<CashMachineDto> getCashMachines = [];

			var listChains = _db.CashCashMachines
				.AsNoTracking()
				.Where(cm => cm.CashId == cashId)
				.ToList();

			foreach (var chain in listChains)
			{
				var cashMachine = await _db.CashMachines.AsNoTracking().FirstOrDefaultAsync(cm => cm.Id == chain.CashMachineId);

				getCashMachines.Add(new CashMachineDto
				{
					Id = cashMachine.Id,
					Serial = cashMachine.Serial,
					Number = cashMachine.Number,
					StartWorking = chain.StartWorking,
					EndWorking = chain.EndWorking
				});
			}
			return getCashMachines;
		}

		public async Task<List<CashMachineDto>> GetAllCashMachinesAsync()
		{
			var getAllMachines = await _db.CashMachines
			.AsNoTracking()
			.OrderBy(x => x.Id)
			.ToListAsync();

			List<CashMachineDto> allMachines = [];

			if (getAllMachines.Count != 0)
			{
				foreach (var cashMachine in getAllMachines)
				{
					var lastConnectionWithCash = await _db.CashCashMachines
						.AsNoTracking()
						.Where(c => c.CashMachineId == cashMachine.Id)
						.OrderByDescending(x => x.Id)
						.FirstOrDefaultAsync();

					if (lastConnectionWithCash != null)
						allMachines.Add(new CashMachineDto
						{
							Id = cashMachine.Id,
							Serial = cashMachine.Serial,
							Number = cashMachine.Number,
							CashId = cashMachine.Id,
							StartWorking = lastConnectionWithCash.StartWorking,
							EndWorking = lastConnectionWithCash.EndWorking
						});
				}
			}
			return allMachines;
		}

		public async Task<List<CashMachineDto>> GetCashMachineBySerialOrNumberAsync(string serial, string number)
		{
			return await _db.CashMachines
				.AsNoTracking()
				.Where(cm=>cm.Serial == serial ||  cm.Number == number)
				.Select(cm => new CashMachineDto
				{
					Id = cm.Id,
					Serial = cm.Serial,
					Number = cm.Number
				})
				.ToListAsync();							
		}
	}
}

using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	///<inheritdoc/>
	public class CashesService(IApplicationDbContext db) : ICashesService
	{
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<int> AddCashAsync(CashDto cashDto)
		{
			ArgumentNullException.ThrowIfNull(cashDto);

			var newCash = new Cash
			{
				Name = cashDto.Name,
				DepartmentId = cashDto.DepartmentId,
				WorkStatus = true
			};

			_db.Cashes.Add(newCash);
			await _db.SaveChangesAsync();

			return newCash.Id;
		}

		public async Task AddCashMachineToCashAsync(CashMachineDto cashMachineDto)
		{
			ArgumentNullException.ThrowIfNull(cashMachineDto);

			var time = DateTime.Now.ToUniversalTime();

			var prevousRelative = await _db.CashCashMachines
				.Where(c => c.CashId == cashMachineDto.CashId)
				.OrderByDescending(x => x.Id)
				.FirstOrDefaultAsync();

			if (prevousRelative != null)
			{
				if (prevousRelative.EndWorking == null)
				{
					prevousRelative.EndWorking = time;
					await _db.SaveChangesAsync();
				}
			}

			_db.CashCashMachines.Add(new CashCashMachine
			{
				CashId = cashMachineDto.CashId,
				CashMachineId = cashMachineDto.Id,
				StartWorking = time
			});
			await _db.SaveChangesAsync();
		}

		public async Task ChangeCashStatusAsync(int id)
		{
			var cash = await _db.Cashes.FirstOrDefaultAsync(c => c.Id == id);

			if (cash != null)
			{
				var lastConnection = await _db.CashCashMachines
						.OrderByDescending(cm => cm.Id)
						.FirstOrDefaultAsync(cm => cm.CashId == cash.Id);

				if (lastConnection != null)
				{
					if (cash.WorkStatus)
					{
						if (lastConnection.EndWorking == null)
						{
							var time = DateTime.Now.ToUniversalTime();
							lastConnection.EndWorking = time;
						}
					}
					else
					{
						var lastConnectionforCashMachine = await _db.CashCashMachines
								.OrderByDescending(cm => cm.Id)
								.FirstOrDefaultAsync(cm => cm.CashMachineId == lastConnection.CashMachineId);

						if (lastConnectionforCashMachine == lastConnection)
						{
							lastConnection.EndWorking = null;
						}

					}
				}
				cash.WorkStatus = !cash.WorkStatus;
				await _db.SaveChangesAsync();
			}
		}

		public async Task<bool> DeleteCashAsync(CashDto cashDto)
		{
			ArgumentNullException.ThrowIfNull(cashDto);

			var checkRelative = await _db.CashCashMachines
				.AsNoTracking()
				.FirstOrDefaultAsync(cm => cm.CashId == cashDto.Id);

			if (checkRelative != null)
				return false;

			var cash = await _db.Cashes.FirstOrDefaultAsync(c => c.Id == cashDto.Id);
			if (cash != null)
			{
				_db.Cashes.Remove(cash);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> CheckCashForDelitingAsync(CashDto cashDto)
		{
			ArgumentNullException.ThrowIfNull(cashDto);

			var checkRelative = await _db.CashCashMachines
				.AsNoTracking()
				.FirstOrDefaultAsync(cm => cm.CashId == cashDto.Id);

			if (checkRelative != null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public async Task EditCashAsync(CashDto cashDto)
		{
			ArgumentNullException.ThrowIfNull(cashDto);

			var cash = await _db.Cashes.FirstOrDefaultAsync(c => c.Id == cashDto.Id);
			if (cash != null)
			{
				cash.Name = cashDto.Name;

				await _db.SaveChangesAsync();
			}
		}

		public async Task<CashDto>? GetCashByIdAsync(int id)
		{
			var cash = await _db.Cashes
				.AsNoTracking()
				.Where(c => c.Id == id)
				.Select(c => new CashDto
				{
					Id = c.Id,
					Name = c.Name,
					DepartmentId = c.DepartmentId,
					IsActive = c.WorkStatus
				})
				.FirstOrDefaultAsync();

			return cash;
		}

		public async Task<List<CashDto>> GetCashesByDepartmentAsync(int id)
		{
			var cashes = await _db.Cashes
				.AsNoTracking()
				.Where(c => c.DepartmentId == id)
				.Select(c => new CashDto
				{
					Id = c.Id,
					Name = c.Name,
					DepartmentId = c.DepartmentId,
					IsActive = c.WorkStatus
				})
				.ToListAsync();

			return cashes;
		}


	}
}

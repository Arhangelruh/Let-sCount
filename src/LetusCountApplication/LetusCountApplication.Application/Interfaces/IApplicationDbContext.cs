using LetusCountApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Interfaces;

public interface IApplicationDbContext
{
	DbSet<Department> Departments { get; }
	DbSet<Cash> Cashes { get; }
	DbSet<CashMachine> CashMachines { get; }
	DbSet<CashCashMachine> CashCashMachines { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
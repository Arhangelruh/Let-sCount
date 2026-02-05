using LetusCountApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Interfaces;

public interface IApplicationDbContext
{
	/// <summary>
	/// Departments.
	/// </summary>
	DbSet<Department> Departments { get; }

	/// <summary>
	/// Cashes.
	/// </summary>
	DbSet<Cash> Cashes { get; }

	/// <summary>
	/// CashMachines.
	/// </summary>
	DbSet<CashMachine> CashMachines { get; }

	/// <summary>
	/// Connecting table between Cashes and CashMachines.
	/// </summary>
	DbSet<CashCashMachine> CashCashMachines { get; }

	/// <summary>
	/// Operations.
	/// </summary>
	DbSet<Operation> Operations { get; }

	/// <summary>
	/// OperationUnits.
	/// </summary>
	DbSet<OperationUnit> OperationUnits { get; }

	/// <summary>
	/// Banknotes.
	/// </summary>
	DbSet<Banknote> Banknotes { get; }


	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
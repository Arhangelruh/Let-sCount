using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Application.QueryModels;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	///<inheritdoc/>
	public class SearchService(
		IBanknotesService banknotesService,
		IApplicationDbContext db,
		IOperationService operationService
		) : ISearchService
	{
		private readonly IBanknotesService _banknotesService = banknotesService ?? throw new ArgumentNullException(nameof(banknotesService));
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));
		private readonly IOperationService _operationService = operationService ?? throw new ArgumentNullException(nameof(operationService));

		public async Task<List<FullTransactionDTO>> SearchByBanknoteAsync(string serialNumber)
		{
			var banknotes = await _banknotesService.GetBySerialAsync(serialNumber);
			List<FullTransactionDTO> transactions = [];

			if (banknotes.Count > 0)
			{
				foreach (var banknote in banknotes)
				{
					var department = await GetDepartmentAsync(banknote.MachineSerial, banknote.EndTime);
					transactions.Add(new FullTransactionDTO
					{
						Department = department,
						Banknote = banknote
					});
				}
			}

			return transactions;
		}

		public async Task<FullOperationInformationDto> GetOperationInformationAsync(int id)
		{
			var operation = await _operationService.GetOperationByIdAsync(id);

			if (operation != null)
			{
				var department = await GetDepartmentTransactionAsync(operation.MachineSerial, operation.EndTime);

				return new FullOperationInformationDto { Operation = operation, Department = department };
			}
			else
			{
				return null;
			}
		}

		private async Task<TransactionDepartmentDto>? GetDepartmentTransactionAsync(string machineSerial, DateTimeOffset dateTime) {
			
			var departmentInformation = await _db.CashCashMachines
				  .Where(ccm =>
				  ccm.CashMachine.Serial == machineSerial &&
				  ccm.StartWorking <= dateTime &&
				  (ccm.EndWorking == null || ccm.EndWorking >= dateTime))
				 .Select(ccm => new TransactionDepartmentDto
				 {
					 Id = ccm.Cash.Department.Id,
					 Name = ccm.Cash.Department.Name,
					 Address = ccm.Cash.Department.Address,
					 IsActive = ccm.Cash.Department.WorkStatus,

					 Cashe = new CashDto
					 {
						 Id = ccm.Cash.Id,
						 Name = ccm.Cash.Name,
						 DepartmentId = ccm.Cash.DepartmentId,
						 IsActive = ccm.Cash.WorkStatus
					 }
				 })
				.FirstOrDefaultAsync();

			return departmentInformation;
		}

		private async Task<DepartmentDto>? GetDepartmentAsync(string machineSerial, DateTimeOffset dateTime)
		{
			var departmentInformation = await _db.CashCashMachines
				.Where(ccm =>
				ccm.CashMachine.Serial == machineSerial &&
				ccm.StartWorking <= dateTime &&
				(ccm.EndWorking == null || ccm.EndWorking >= dateTime))
			   .Select(ccm => new DepartmentDto
			   {
				   Id = ccm.Cash.Department.Id,
				   Name = ccm.Cash.Department.Name,
				   Address = ccm.Cash.Department.Address,
				   IsActive = ccm.Cash.Department.WorkStatus,

				   Cashes = new List<CashDto>
				  {
						 new CashDto
						 {
						 Id = ccm.Cash.Id,
						 Name = ccm.Cash.Name,
						 DepartmentId = ccm.Cash.DepartmentId,
						 IsActive = ccm.Cash.WorkStatus
						 }
				  }
			   })
			  .FirstOrDefaultAsync();

			return departmentInformation;
		}

		public async Task<PagedResult<ShortOperationInformationDto>> SearchByDepartment(DepartmentOperationQuery query)
		{
			var baseQuery =
			from ou in _db.OperationUnits
			join op in _db.Operations
				on ou.OperationId equals op.Id
			join cm in _db.CashMachines
				on op.MachineSerial equals cm.Serial
			join ccm in _db.CashCashMachines
				on cm.Id equals ccm.CashMachineId
			join cash in _db.Cashes
				on ccm.CashId equals cash.Id
			join dep in _db.Departments
				on cash.DepartmentId equals dep.Id
			where cash.DepartmentId == query.DepartmentId
				  && op.EndTime >= query.DateFrom
				  && op.EndTime <= query.DateTo
				  && op.StartTime >= ccm.StartWorking
				  && (ccm.EndWorking == null || op.StartTime <= ccm.EndWorking)
			select new ShortOperationInformationDto
			{
				Department = new TransactionDepartmentDto
				{
					Id = dep.Id,
					Name = dep.Name,
					Address = dep.Address,
					IsActive = dep.WorkStatus,
					Cashe = new CashDto
					{
						Id = cash.Id,
						Name = cash.Name,
						DepartmentId = cash.DepartmentId,
						IsActive = cash.WorkStatus
					}
				},
				Operation = new TransactionOperationDto
				{
					Id = op.Id,
					MachineSerial = op.MachineSerial,
					StartTime = op.StartTime,
					EndTime = op.EndTime,
					OperationUnit = new OperationUnitDto
					{
						Id = ou.Id,
						Currency = ou.Currency,
						TotalSum = ou.TotalSum,
						Banknotes = new List<BanknoteDto>()
					}
				}
			};

			baseQuery = query.SortField switch
			{
				"CashId" => query.SortDirection == "asc"
					? baseQuery.OrderBy(x => x.Department.Cashe.Id)
					: baseQuery.OrderByDescending(x => x.Department.Cashe.Id),

				"Currency" => query.SortDirection == "asc"
					? baseQuery.OrderBy(x => x.Operation.OperationUnit.Currency)
					: baseQuery.OrderByDescending(x => x.Operation.OperationUnit.Currency),

				"TotalSum" => query.SortDirection == "asc"
					? baseQuery.OrderBy(x => x.Operation.OperationUnit.TotalSum)
					: baseQuery.OrderByDescending(x => x.Operation.OperationUnit.TotalSum),

				_ => query.SortDirection == "asc"
					? baseQuery.OrderBy(x => x.Operation.EndTime)
					: baseQuery.OrderByDescending(x => x.Operation.EndTime)
			};

			var totalCount = await baseQuery.CountAsync();

			var items = await baseQuery
				.Skip((query.Page - 1) * query.PageSize)
				.Take(query.PageSize)
				.ToListAsync();

			return new PagedResult<ShortOperationInformationDto>(items, totalCount, query.Page, query.PageSize);

		}
	}
}

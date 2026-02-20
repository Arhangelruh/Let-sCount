using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
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
				var department = await GetDepartmentAsync(operation.MachineSerial, operation.EndTime);

				return new FullOperationInformationDto { Operation = operation, Department = department };
			}
			else
			{
				return null;
			}
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
	}
}

using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LetusCountApplication.Application.Services
{
	///<inheritdoc/>
	public class SearchService(IBanknotesService banknotesService, IApplicationDbContext db) : ISearchService
	{
		private readonly IBanknotesService _banknotesService = banknotesService ?? throw new ArgumentNullException(nameof(banknotesService));
		private readonly IApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

		public async Task<List<FullTransactionDTO>> SearchByBanknote(string serialNumber)
		{
			var banknotes = await _banknotesService.GetBySerialAsync(serialNumber);
			List<FullTransactionDTO> transactions = [];

			if (banknotes.Count > 0)
			{
				foreach (var banknote in banknotes)
				{
					var departmentInformation = await _db.CashCashMachines
	                .Where(ccm =>
		            ccm.CashMachine.Serial == banknote.MachineSerial &&
		            ccm.StartWorking <= banknote.EndTime &&
		            (ccm.EndWorking == null || ccm.EndWorking >= banknote.EndTime))
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

					transactions.Add(new FullTransactionDTO
					{
						Department = departmentInformation,
						Banknote = banknote
					});
				}
			}

			return transactions;
		}
	}
}

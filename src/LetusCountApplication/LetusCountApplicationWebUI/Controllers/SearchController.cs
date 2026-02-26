using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Application.QueryModels;
using LetusCountApplication.Application.Services;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LetusCountApplicationWebUI.Controllers
{
	public class SearchController(
		ISearchService searchService,
		IDepartmentsService departmentsService,
		ICashesService cashesService
		) : Controller
	{
		private readonly ISearchService _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
		private readonly IDepartmentsService _departmentsService = departmentsService ?? throw new ArgumentNullException(nameof(departmentsService));
		private readonly ICashesService _cashesService = cashesService ?? throw new ArgumentNullException(nameof(cashesService));

		/// <summary>
		/// Get information by banknote serial number.
		/// </summary>
		/// <param name="serial"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> SearchByBanknote(string serial)
		{
			List<BanknoteViewModel> banknotes = [];

			if (serial != null)
			{
				var operations = await _searchService.SearchByBanknoteAsync(serial);

				if (operations.Count > 0)
				{
					foreach (var operation in operations)
					{
						banknotes.Add(new BanknoteViewModel
						{
							Department = operation.Department != null ? operation.Department.Name : "Не найден",
							Cash = operation.Department != null && operation.Department.Cashes != null ? operation.Department.Cashes[0].Name : "Не найдена",
							MachineSerial = operation.Banknote.MachineSerial,
							Currency = operation.Banknote.Currency,
							DenomName = operation.Banknote.DenomName,
							Value = operation.Banknote.Value,
							OperationId = operation.Banknote.OperationId,
							SerialNumber = operation.Banknote.SerialNumber,
							StartTime = operation.Banknote.StartTime.ToLocalTime(),
							EndTime = operation.Banknote.EndTime.ToLocalTime()
						});
					}
				}
			}
			banknotes = banknotes.OrderByDescending(b => b.EndTime).ToList();

			return View(banknotes);
		}

		[HttpGet]
		public async Task<IActionResult> SearchByDepartment(DepartmentOperationsViewModel model)
		{
			var department = await _departmentsService.GetDepartmentByIdAsync(model.DepartmentId);

			if (department == null)
			{
				ViewBag.ErrorMessage = "Депортамент не найден.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}

			model.Department = department.Name;

			var query = new DepartmentOperationQuery
			{
				DepartmentId = model.DepartmentId,
				DateFrom = model.DateTimeFrom.ToUniversalTime(),
				DateTo = model.DateTimeTo.ToUniversalTime(),
				SortField = model.SortField,
				SortDirection = model.SortDirection,
				Page = model.Page < 1 ? 1 : model.Page,
				PageSize = model.PageSize <= 0 ? 100 : model.PageSize
			};

			var result = await _searchService.SearchByDepartment(query);			  

			List<OperationViewModel> operations = [];
			foreach (var operation in result.Items)
			{
				operations.Add(
					  new OperationViewModel
					  {
						  Cash = operation.Department.Cashe.Name,
						  MachineSerial = operation.Operation.MachineSerial,
						  EndTime = operation.Operation.EndTime.ToLocalTime(),
						  OperationUnits = [
						  new OperationUnitViewModel {
							 OperationId = operation.Operation.Id,
							 Currency = operation.Operation.OperationUnit.Currency,
							 Sum = operation.Operation.OperationUnit.TotalSum
						  }]
					  }
				);
			}

			model.Operations = new PagedResult<OperationViewModel>(operations, result.TotalCount, result.Page, result.PageSize);

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> SearchByCash(CashOperationViewModel model)
		{
			var cash = await _cashesService.GetCashByIdAsync(model.CashId);

			if(cash == null)
			{
				ViewBag.ErrorMessage = "Касса не найдена.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}

			var department = await _departmentsService.GetDepartmentByIdAsync(cash.DepartmentId);

			var query = new CashOperationQuery
			{
				CashId = cash.Id,
				DateFrom = model.DateTimeFrom.ToUniversalTime(),
				DateTo = model.DateTimeTo.ToUniversalTime(),
				SortField = model.SortField,
				SortDirection = model.SortDirection,
				Page = model.Page < 1 ? 1 : model.Page,
				PageSize = model.PageSize <= 0 ? 100 : model.PageSize
			};

			var result = await _searchService.SearchByCash(query);
			
			model.Cash = cash.Name;
			model.Department = department.Name;

			List<OperationViewModel> operations = [];

			foreach (var operation in result.Items)
			{
				operations.Add(
					  new OperationViewModel
					  {
						  Department = department.Name,
						  Cash = operation.Cash.Name,
						  MachineSerial = operation.Operation.MachineSerial,
						  EndTime = operation.Operation.EndTime.ToLocalTime(),
						  OperationUnits = [
						  new OperationUnitViewModel {
							 OperationId = operation.Operation.Id,
							 Currency = operation.Operation.OperationUnit.Currency,
							 Sum = operation.Operation.OperationUnit.TotalSum
						  }]
					  }
				);
			}
			model.Operations = new PagedResult<OperationViewModel>(operations, result.TotalCount, result.Page, result.PageSize);

			return View(model);
		}
	}
}

using LetusCountApplication.Application.Interfaces;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetusCountApplicationWebUI.Controllers
{
	public class OperationController(ISearchService searchService) : Controller
	{
		private readonly ISearchService _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));

		/// <summary>
		/// Get information about operation.
		/// </summary>
		/// <param name="operationId"></param>
		/// <returns></returns>
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Operation(int operationId)
		{

			var operation = await _searchService.GetOperationInformationAsync(operationId);

			if (operation != null)
			{

				List<OperationUnitViewModel> operationUnits = [];

				if (operation.Operation.OperationUnits != null)
				{
					foreach (var unit in operation.Operation.OperationUnits)
					{
						List<OperationBanknoteViewModel> banknotes = [];

						if (unit.Banknotes != null)
						{
							foreach (var banknote in unit.Banknotes)
							{
								banknotes.Add(new OperationBanknoteViewModel
								{
									SerialNumber = banknote.SerialNumber,
									DenomName = banknote.DenomName,
									Value = banknote.Value
								});
							}
						}

						operationUnits.Add(new OperationUnitViewModel
						{
							Currency = unit.Currency,
							Sum = unit.TotalSum,
							operationBanknotes = [.. banknotes.OrderBy(b => b.Value)]
						});
					}
				}

				var model = new OperationViewModel
				{
					Department = operation.Department != null ? operation.Department.Name : "Не найден",
					Cash = operation.Department != null && operation.Department.Cashe != null ? operation.Department.Cashe.Name : "Не найдена",
					MachineSerial = operation.Operation.MachineSerial,
					EndTime = operation.Operation.EndTime.ToLocalTime(),
					StartTime = operation.Operation.StartTime.ToLocalTime(),
					OperationUnits = operationUnits
				};
				return View(model);
			}

			return View();
		}
	}
}

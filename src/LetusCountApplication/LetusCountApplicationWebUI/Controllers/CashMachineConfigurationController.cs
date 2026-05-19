using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Constants;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetusCountApplicationWebUI.Controllers
{
	public class CashMachineConfigurationController(ICashMachinesService cashMachinesService) : Controller
	{
		private readonly ICashMachinesService _cashMachinesService = cashMachinesService ?? throw new ArgumentNullException(nameof(cashMachinesService));

		/// <summary>
		/// Get cash machines.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> CashMachines()
		{
			var getAllCashMachines = await _cashMachinesService.GetAllCashMachinesAsync();

			List<CashMachineViewModel> cashMachineViewModels = [];

			foreach (var cashMachine in getAllCashMachines)
			{
				cashMachineViewModels.Add(new CashMachineViewModel
				{
					Id = cashMachine.Id,
					Serial = cashMachine.Serial,
					Number = cashMachine.Number,
					IsActive = cashMachine.StartWorking == null ? false : true,
				});
			}

			return View(cashMachineViewModels);
		}

		/// <summary>
		/// Model for creating a cash machine.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public IActionResult AddCashMachine()
		{
			return View();
		}

		/// <summary>
		/// Add new check machine.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> AddCashMachine(CashMachineViewModel model)
		{
			if (ModelState.IsValid)
			{
				var checkMachines = await _cashMachinesService.GetCashMachineBySerialOrNumberAsync(model.Serial, model.Number);
				if (checkMachines.Count == 0)
				{
					await _cashMachinesService.AddCashMachineAsync(new CashMachineDto
					{
						Serial = model.Serial,
						Number = model.Number
					});
					return RedirectToAction("CashMachines");
				}
				else
				{
					ModelState.AddModelError("Error", "Kisan с таким серийным или инвентарным номером уже есть в системе.");
				}
			}
			return View(model);
		}

		/// <summary>
		/// Delete cash machine.
		/// </summary>
		/// <param name="machineId">Cash machine id</param>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> DeleteCashMachine(int machineId)
		{
			var cashMachine = await _cashMachinesService.GetCashMachineByIdAsync(machineId);
			if (cashMachine != null)
			{
				var result = await _cashMachinesService.DeleteCashMachineAsync(cashMachine.Id);
				if (result)
				{
					return RedirectToAction("CashMachines");
				}
				ViewBag.ErrorMessage = "Обратите внимание что нельзя удалить Kisan который уже привязан к кассе и находиться в работе.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}
			ViewBag.ErrorMessage = "Kisan не найден.";
			ViewBag.ErrorTitle = "Ошибка";
			return View("~/Views/Error/Error.cshtml");
		}
	}
}

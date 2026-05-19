using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Constants;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LetusCountApplicationWebUI.Controllers
{
	public class CashController(ICashMachinesService cashMachinesService, ICashesService cashesService) : Controller
	{
		private readonly ICashMachinesService _cashMachinesService = cashMachinesService ?? throw new ArgumentNullException(nameof(cashMachinesService));
		private readonly ICashesService _cashesService = cashesService ?? throw new ArgumentNullException(nameof(cashesService));

		/// <summary>
		/// Add cash page.
		/// </summary>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> AddCash(int departmentId)
		{
			var cashMachines = await _cashMachinesService.GetAllAvailableCashMachinesAsync();

			var model = new CashViewModel
			{
				Name = string.Empty,
				DepartmentId = departmentId,
				CashMachines = cashMachines.Select(cm => new SelectListItem
				{
					Value = cm.Id.ToString(),
					Text = $"{cm.Serial} (инв.N {cm.Number})"
				}).ToList()
			};

			return View(model);
		}

		/// <summary>
		/// Add new cash.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> AddCash(CashViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cash = new CashDto
				{
					Name = model.Name,
					DepartmentId = model.DepartmentId,
					IsActive = true
				};
				var cashId = await _cashesService.AddCashAsync(cash);

				if (model.SelectedCashMachineId != null)
				{
					int cashMachineId = model.SelectedCashMachineId ?? 0;
					var cashMachine = await _cashMachinesService.GetCashMachineByIdAsync(cashMachineId);

					if (cashMachine != null)
					{
						cashMachine.CashId = cashId;
						await _cashesService.AddCashMachineToCashAsync(cashMachine);
					}
					else
					{
						ViewBag.ErrorMessage = "Не найден Kisan.";
						ViewBag.ErrorTitle = "Ошибка";
						return View("~/Views/Error/Error.cshtml");
					}
				}
				return RedirectToAction("Departments", "Department");
			}

			var cashMachines = await _cashMachinesService.GetAllAvailableCashMachinesAsync();

			model.CashMachines = cashMachines.Select(cm => new SelectListItem
			{
				Value = cm.Id.ToString(),
				Text = $"{cm.Serial} (инв.N {cm.Number})"
			}).ToList();

			return View(model);
		}

		/// <summary>
		/// Edit cash page.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> EditCash(int cashId)
		{
			var cash = await _cashesService.GetCashByIdAsync(cashId);

			if (cash != null)
			{
				var cashMachines = await _cashMachinesService.GetAllAvailableCashMachinesAsync();
				var currentMachine = await _cashMachinesService.GetConnectedToCashCashMachineAsync(cash.Id);

				var model = new CashViewModel
				{
					Id = cash.Id,
					Name = cash.Name,
					DepartmentId = cash.DepartmentId,
					CashMachines = cashMachines.Select(cm => new SelectListItem
					{
						Value = cm.Id.ToString(),
						Text = $"{cm.Serial} (инв.N {cm.Number})"
					}).ToList()
				};

				if (currentMachine != null)
				{
					model.CashMachine = new CashMachineViewModel
					{
						Id = currentMachine.Id,
						Number = currentMachine.Number,
						Serial = currentMachine.Serial
					};
				}
				return View(model);
			}
			else
			{
				ViewBag.ErrorMessage = "Не найден касса.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}
		}

		/// <summary>
		/// Edit cash.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> EditCash(CashViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cash = await _cashesService.GetCashByIdAsync(model.Id);

				if (cash != null)
				{
					var changeDto = new CashDto
					{
						Id = cash.Id,
						Name = model.Name,
						DepartmentId = cash.DepartmentId,
						IsActive = cash.IsActive
					};

					await _cashesService.EditCashAsync(changeDto);

					if (model.SelectedCashMachineId != null)
					{
						var currentMachine = await _cashMachinesService.GetConnectedToCashCashMachineAsync(cash.Id);

						int cashMachineId = model.SelectedCashMachineId ?? 0;
						var cashMachine = await _cashMachinesService.GetCashMachineByIdAsync(cashMachineId);

						if (currentMachine != null)
						{
							if (currentMachine.Id == cashMachine.Id)
							{
								return RedirectToAction("Departments", "Department");
							}
						}

						cashMachine.CashId = cash.Id;
						await _cashesService.AddCashMachineToCashAsync(cashMachine);
					}
					return RedirectToAction("Departments", "Department");
				}
				else
				{
					ViewBag.ErrorMessage = "Не найден касса.";
					ViewBag.ErrorTitle = "Ошибка";
					return View("~/Views/Error/Error.cshtml");
				}
			}

			var cashMachines = await _cashMachinesService.GetAllAvailableCashMachinesAsync();

			model.CashMachines = cashMachines.Select(cm => new SelectListItem
			{
				Value = cm.Id.ToString(),
				Text = $"{cm.Serial} (инв.N {cm.Number})"
			}).ToList();

			return View(model);
		}

		/// <summary>
		/// Change cash status.
		/// </summary>
		/// <param name="cashId"></param>		
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<ActionResult> ChangeStatus(int cashId)
		{
			var cash = await _cashesService.GetCashByIdAsync(cashId);
			if (cash != null)
			{
				await _cashesService.ChangeCashStatusAsync(cash.Id);
			}
			return RedirectToAction("Departments", "Department");
		}

		/// <summary>
		/// Delete cash.
		/// </summary>
		/// <param name="cashId">Cash id</param>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<ActionResult> DeleteCash(int cashId)
		{
			var cash = await _cashesService.GetCashByIdAsync(cashId);

			if (cash != null)
			{
				var result = await _cashesService.DeleteCashAsync(cash);
				if (result)
				{
					return RedirectToAction("Departments", "Department");
				}
				ViewBag.ErrorMessage = "Обратите внимание что нельзя удалить кассу в которая уже была привязанна к Kisan.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}
			ViewBag.ErrorMessage = "Касса не найдена.";
			ViewBag.ErrorTitle = "Ошибка";
			return View("~/Views/Error/Error.cshtml");
		}
	}
}

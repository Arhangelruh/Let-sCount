using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplication.Domain.Constants;
using LetusCountApplicationWebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetusCountApplicationWebUI.Controllers
{
	public class DepartmentController(
		IDepartmentsService departmentsService,
		ICashMachinesService cashMachinesService,
		ICashesService cashesService
		) : Controller
	{
		private readonly IDepartmentsService _departmentsService = departmentsService ?? throw new ArgumentNullException(nameof(departmentsService));
		private readonly ICashMachinesService _cashMachinesService = cashMachinesService ?? throw new ArgumentNullException(nameof(cashMachinesService));
		private readonly ICashesService _cashesService = cashesService ?? throw new ArgumentNullException(nameof(cashesService));

		/// <summary>
		/// Get all departmens with cashes.
		/// </summary>		
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> Departments()
		{
			var departments = await _departmentsService.GetAllDepartmentsAsync();
			List<DepartmentViewModel> departmentViewModels = [];

			if (departments.Count > 0)
			{
				foreach (var department in departments)
				{
					List<CashViewModel> cashViewModels = [];
					if (department.Cashes.Count > 0)
					{
						foreach (var cash in department.Cashes)
						{
							var cashMachine = await _cashMachinesService.GetConnectedToCashCashMachineAsync(cash.Id);

							cashViewModels.Add(new CashViewModel
							{
								Id = cash.Id,
								Name = cash.Name,
								IsActive = cash.IsActive,
								CashMachine = cashMachine != null ? new CashMachineViewModel { Id = cashMachine.Id, Serial = cashMachine.Serial, Number = cashMachine.Number } : null
							});
						}
					}

					departmentViewModels.Add(new DepartmentViewModel
					{
						Id = department.Id,
						Name = department.Name,
						IsActive = department.IsActive,
						Address = department.Address,
						Cashes = cashViewModels
					});
				}
			}

			return View(departmentViewModels.OrderByDescending(d => d.IsActive).ThenBy(d => d.Id).ToList());
		}

		/// <summary>
		/// Add department page.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public ActionResult AddDepartment()
		{
			return View();
		}

		/// <summary>
		/// Add new department.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<ActionResult> AddDepartment(DepartmentViewModel model)
		{
			if (ModelState.IsValid)
			{
				var department = new DepartmentDto
				{
					Name = model.Name,
					Address = model.Address,
					IsActive = true,
				};
				await _departmentsService.AddDepartmentAsync(department);
				return RedirectToAction("Departments");
			}
			return View(model);
		}

		/// <summary>
		/// Change department status.
		/// </summary>
		/// <param name="departmentId"></param>		
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<ActionResult> ChangeStatus(int departmentId)
		{
			var department = await _departmentsService.GetDepartmentByIdAsync(departmentId);
			if (department != null)
			{
				if (department.IsActive)
				{
					var cashes = await _cashesService.GetCashesByDepartmentAsync(department.Id);
					if (cashes != null)
					{
						foreach (var cash in cashes)
						{
							if (cash.IsActive)
							{
								await _cashesService.ChangeCashStatusAsync(cash.Id);
							}
						}
					}
				}
				await _departmentsService.ChangeDepartmentStatusAsync(department.Id);
			}
			return RedirectToAction("Departments");
		}

		/// <summary>
		/// Delete department.
		/// </summary>
		/// <param name="departmentId">Department id</param>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<ActionResult> DeleteDepartment(int departmentId)
		{
			var department = await _departmentsService.GetDepartmentByIdAsync(departmentId);
			if (department != null)
			{
				var result = await _departmentsService.DeleteDepartmentAsync(department.Id);
				if (result)
				{
					return RedirectToAction("Departments");
				}
				ViewBag.ErrorMessage = "Обратите внимание что нельзя удалить подразделение в которых есть кассы уже привязанные к Kisan.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}
			ViewBag.ErrorMessage = "Подразделение не найдено.";
			ViewBag.ErrorTitle = "Ошибка";
			return View("~/Views/Error/Error.cshtml");
		}

		/// <summary>
		/// Edit department view.
		/// </summary>
		/// <param name="departmentId"></param>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> EditDepartment(int departmentId)
		{

			var department = await _departmentsService.GetDepartmentByIdAsync(departmentId);
			if (department != null)
			{
				var model = new DepartmentViewModel
				{
					Id = department.Id,
					Name = department.Name,
					Address = department.Address
				};
				return View(model);
			}
			else
			{
				ViewBag.ErrorMessage = "Депортамент не найден.";
				ViewBag.ErrorTitle = "Ошибка";
				return View("~/Views/Error/Error.cshtml");
			}
		}

		/// <summary>
		/// Edit department.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = Roles.Admin)]
		public async Task<IActionResult> EditDepartment(DepartmentViewModel model)
		{
			if (ModelState.IsValid)
			{
				var department = await _departmentsService.GetDepartmentByIdAsync(model.Id);
				if (department != null)
				{
					var departmentDto = new DepartmentDto
					{
						Id = department.Id,
						Name = model.Name,
						Address = model.Address,
						IsActive = department.IsActive
					};

					await _departmentsService.UpdateDepartmentAsync(departmentDto);

					return RedirectToAction("Departments");
				}
				else
				{
					ViewBag.ErrorMessage = "Депортамент не найден.";
					ViewBag.ErrorTitle = "Ошибка";
					return View("~/Views/Error/Error.cshtml");
				}
			}
			return View(model);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetActiveDepartments()
		{
			var departments = await _departmentsService.GetAllActiveDepartmentsAsync();
			List<DepartmentViewModel> departmentViewModels = [];

			if (departments.Count > 0)
			{
				foreach (var department in departments)
				{
					List<CashViewModel> cashViewModels = [];
					if (department.Cashes.Count > 0)
					{
						foreach (var cash in department.Cashes)
						{
							cashViewModels.Add(new CashViewModel
							{
								Id = cash.Id,
								Name = cash.Name,
								IsActive = cash.IsActive,
							});
						}
					}

					departmentViewModels.Add(new DepartmentViewModel
					{
						Id = department.Id,
						Name = department.Name,
						IsActive = department.IsActive,
						Address = department.Address,
						Cashes = cashViewModels
					});
				}
			}

			return View(departmentViewModels.OrderBy(d => d.Id).ToList());
		}
	}
}

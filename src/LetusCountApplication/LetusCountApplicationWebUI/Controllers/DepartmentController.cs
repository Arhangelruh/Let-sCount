using LetusCountApplication.Application.DTOModels;
using LetusCountApplication.Application.Interfaces;
using LetusCountApplicationWebUI.ViewModels;
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
		public async Task<IActionResult> Departments()
		{
			var departments = await _departmentsService.GetAllDepartmentsAsync();
			List<DepartmentViewModel> departmentViewModels = [];

			if (departments.Count != 0)
			{
				foreach (var department in departments)
				{
					List<CashViewModel> cashViewModels = [];
					if (department.Cashes.Count != 0)
					{
						foreach (var cash in department.Cashes)
						{							
							var cashMachine = await _cashMachinesService.GetConnectedToCashCashMachineAsync(cash.Id);
							if(cashMachine != null) { 
							}

							cashViewModels.Add(new CashViewModel
							{
								Id = cash.Id,
								Name = cash.Name,
								IsActive = cash.IsActive,
								CashMachine = cashMachine != null ? new CashMachineViewModel{ Id = cashMachine.Id, Serial = cashMachine.Serial, Number = cashMachine.Number } : null
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
		public async Task<ActionResult> ChangeStatus(int departmentId)
		{
			var department = await _departmentsService.GetDepartmentByIdAsync(departmentId);
			if (department != null)
			{
				if (department.IsActive) { 
				   var cashes = await _cashesService.GetCashesByDepartmentAsync(department.Id);
					if (cashes != null) {
						foreach (var cash in cashes) {
							if (cash.IsActive) { 
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
	}
}

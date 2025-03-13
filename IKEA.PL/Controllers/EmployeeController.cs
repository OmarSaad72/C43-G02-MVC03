using IKEA.BLL.Models.Common.Enums;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Presistance.Data.Migrations;
using IKEA.PL.View_Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _EmployeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _Env;

        public EmployeeController(IEmployeeService EmployeeService, ILogger<EmployeeController> logger, IWebHostEnvironment webHost)
        {
            _EmployeeService = EmployeeService;
            _logger = logger;
            _Env = webHost;
        }
        [HttpGet] //Default 
        public IActionResult Index() //Master Action
        {
            var dep = _EmployeeService.GetAllEmployees();
            return View(dep);
        }

        [HttpGet] //Show The Form
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var message = string.Empty;
            try
            {
                var result = _EmployeeService.CreateEmployee(dto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee Can't Be Created!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.LogError(ex, ex.Message);
                if (_Env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(dto);
                }
                else
                {
                    message = "Employee Can't Be Created!";
                    return View("Error", message);
                }
            }
        }
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
                return BadRequest();  // 400
            var Employee = _EmployeeService.GetEmployeeById(Id.Value);
            if (Employee == null)
            {
                return NotFound();  // 404
            }
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = _EmployeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();
            return View(new UpdateEmployeeDto()
            {
                EmployeeType = Enum.TryParse<EmployeeType>(employee.EmployeeType, true, out var employeetype) ? employeetype : default,
                Gender = Enum.TryParse<Gender>(employee.Gender, true, out var gender) ? gender : default,
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Id = id.Value,
                Salary = employee.Salary,
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, UpdateEmployeeDto edit)
        {
            if (!ModelState.IsValid)
                return View(edit);
            var message = string.Empty;
            try
            {
                var result = _EmployeeService.UpdateEmployee(edit);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee Can't Be Updated!";
                }
            }
            catch (Exception ex)
            {
                message = _Env.IsDevelopment() ? ex.Message : "Employee Can't Be Updated!";
            }
            return View(edit);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var DeleteDep = _EmployeeService.GetEmployeeById(id.Value);
            if (DeleteDep is null)
                return NotFound();
            return View(DeleteDep);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var DeleteDep = _EmployeeService.DeleteEmployee(id);
            var message = string.Empty;
            try
            {
                if (DeleteDep)
                    return RedirectToAction(nameof(Index));
                message = "An Error Happend, Can't Deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _Env.IsDevelopment() ? ex.Message : "An Error Happend, Can't Deleted";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
    }
}

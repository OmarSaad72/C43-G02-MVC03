using AutoMapper;
using IKEA.BLL.Common.Services.AttachmentService;
using IKEA.BLL.Models.Common.Enums;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.PL.Mapping_Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    //[AllowAnonymous] // default if not exist authorize
    [Authorize] // Any One Authenticated Is Authorize
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _EmployeeService;
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _Env;

        public EmployeeController(IEmployeeService EmployeeService, IAttachmentService attachmentService, IMapper mapper, ILogger<EmployeeController> logger, IWebHostEnvironment webHost)
        {
            _EmployeeService = EmployeeService;
            _attachmentService = attachmentService;
            _mapper = mapper;
            _logger = logger;
            _Env = webHost;
        }
        [HttpGet] //Default 
        public async Task<IActionResult> Index(string SearchValue) //Master Action
        {
            var dep = await _EmployeeService.GetAllEmployeesAsync(SearchValue);
            return View(dep);
        }

        [HttpGet] //Show The Form
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public async Task<IActionResult> Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                //var emp = _mapper.Map<EditCreateEmployeeDto>(employee);
                var result = await _EmployeeService.CreateEmployeeAsync(employee);
                if (result > 0)
                {
                    TempData["Message"] = "Employee Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee Can't Be Created!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.LogError(ex, ex.Message);
                if (_Env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employee);
                }
                else
                {
                    message = "Employee Can't Be Created!";
                    return View("Error", message);
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
                return BadRequest();  // 400
            var Employee = await _EmployeeService.GetEmployeeByIdAsync(Id.Value);
            if (Employee == null)
            {
                return NotFound();  // 404
            }
            return View(Employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = await _EmployeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
                return NotFound();
            var emp = _mapper.Map<EmployeesDetailsReturnDto,EditCreateEmployeeDto>(employee);
            //var emp1 = _mapper.Map<EditCreateEmployeeDto, EditCreateEmployeeDto>(emp);
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public async Task<IActionResult> Edit(int id, EditCreateEmployeeDto edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }
            var message = string.Empty;
            try
            {
                var emp = _mapper.Map<EditCreateEmployeeDto, EditCreateEmployeeDto>(edit);
                var result = await _EmployeeService.UpdateEmployeeAsync(emp);
                if (result > 0)
                {
                    TempData["Message"] = "Employee Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var DeleteDep = await _EmployeeService.GetEmployeeByIdAsync(id.Value);
            if (DeleteDep is null)
                return NotFound();
            return View(DeleteDep);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public async Task<IActionResult> Delete(int id)
        {
            var DeleteDep = await _EmployeeService.DeleteEmployeeAsync(id);
            var message = string.Empty;
            try
            {
                if (DeleteDep)
                {
                    TempData["Message"] = "Employee Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                message = "An Error Happened, Can't Deleted";
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

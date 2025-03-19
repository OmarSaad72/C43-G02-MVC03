using AutoMapper;
using IKEA.BLL.Common.Services.AttachmentService;
using IKEA.BLL.Models.Common.Enums;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.PL.Mapping_Profile;
using IKEA.PL.View_Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
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
        public IActionResult Index(string SearchValue) //Master Action
        {
            var dep = _EmployeeService.GetAllEmployees(SearchValue);
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
        public IActionResult Create(EditCreateEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var emp = _mapper.Map<EditCreateEmployeeDto, EditCreateEmployeeDto>(employee);
                var result = _EmployeeService.CreateEmployee(emp);
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
            var emp = _mapper.Map<EmployeesDetailsReturnDto, EditCreateEmployeeDto>(employee);
            //if (emp.Image != null)
            //{
            //    IFormFile emp1 = _mapper.Map<IFormFile>(emp);
            //    employee.Image = _attachmentService.Upload(emp.Image, "Images");
            //}
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public IActionResult Edit(int id, EditCreateEmployeeDto edit)
        {
            if (!ModelState.IsValid)
                return View(edit);
            var message = string.Empty;
            try
            {
                var emp = _mapper.Map<EditCreateEmployeeDto, EmployeeEditVM>(edit);
                var result = _EmployeeService.UpdateEmployee(edit);
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
        [ValidateAntiForgeryToken] //Action Filter
        public IActionResult Delete(int id)
        {
            var DeleteDep = _EmployeeService.DeleteEmployee(id);
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

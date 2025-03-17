using AutoMapper;
using IKEA.BLL.Models.Department;
using IKEA.BLL.Services.Department;
using IKEA.PL.View_Models.Department;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _Env;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper, ILogger<DepartmentController> logger, IWebHostEnvironment webHost)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _Env = webHost;
        }
        [HttpGet] //Default 
        public IActionResult Index() //Master Action
        {
            ViewData["Message"] = "View Data";
            ViewBag.Message = "View Bag";
            var dep = _departmentService.GetAllDepartments();
            return View(dep);
        }

        [HttpGet] //Show The Form
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public IActionResult Create(DepartmentVM dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var message = string.Empty;
            try
            {
                var department = _mapper.Map<DepartmentVM , CreatedDepartmentDto>(dto);
                var result = _departmentService.CreateDepartment(department);
                if (result > 0)
                {
                    TempData["Message"] = "Department Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department Can't Be Created!";
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
                    message = "Department Can't Be Created!";
                    return View("Error", message);
                }
            }
        }
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
                return BadRequest();  // 400
            var department = _departmentService.GetDepartmentsById(Id.Value);
            if (department == null)
            {
                return NotFound();  // 404
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentService.GetDepartmentsById(id.Value);
            if (department == null)
                return NotFound();
            var department2 = _mapper.Map<DepartmentsDetailsReturnDto, DepartmentVM>(department);
            return View(department2);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public IActionResult Edit(int id, DepartmentVM edit)
        {
            if (!ModelState.IsValid)
                return View(edit);
            var message = string.Empty;
            try
            {
                var department = _mapper.Map<UpdateDepartmentDto>(edit);
                department.Id = id;
                var result = _departmentService.UpdateDepartment(department);
                
                if (result > 0)
                {
                    TempData["Message"] = "Department Updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department Can't Be Updated!";
                }
            }
            catch (Exception ex)
            {
                message = _Env.IsDevelopment() ? ex.Message : "Department Can't Be Updated!";
            }
            return View(edit);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var DeleteDep = _departmentService.GetDepartmentsById(id.Value);
            if (DeleteDep is null)
                return NotFound();
            return View(DeleteDep);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Action Filter
        public IActionResult Delete(int id)
        {
            var DeleteDep = _departmentService.DeleteDepartment(id);
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

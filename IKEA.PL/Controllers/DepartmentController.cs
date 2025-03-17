using IKEA.BLL.Models.Department;
using IKEA.BLL.Services.Department;
using IKEA.PL.View_Models.Department;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _Env;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHost)
        {
            _departmentService = departmentService;
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
                var result = _departmentService.CreateDepartment(new CreatedDepartmentDto
                {
                    Code = dto.Code,
                    Name = dto.Name,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                });
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
            return View(new DepartmentVM()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });
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
                var result = _departmentService.UpdateDepartment(new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = edit.Code,
                    Name = edit.Name,
                    Description = edit.Description,
                    CreationDate = edit.CreationDate
                });
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
            if(DeleteDep is null)
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
                message =_Env.IsDevelopment() ? ex.Message : "An Error Happend, Can't Deleted";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
    }
}

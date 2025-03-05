using IKEA.BLL.Models.Department;
using IKEA.BLL.Services.Department;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _Env;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger ,IWebHostEnvironment webHost)
        {
            _departmentService = departmentService;
            _logger = logger;
            _Env = webHost;
        }
        [HttpGet] //Default 
        public IActionResult Index() //Master Action
        {
            var dep = _departmentService.GetAllDepartments();
            return View(dep);
        }

        [HttpGet] //Show The Form
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(dto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
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
                _logger.LogError(ex,ex.Message);
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
            if(Id == null)
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
            return View(department);
        }
    }
}
